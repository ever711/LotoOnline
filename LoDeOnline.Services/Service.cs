using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Dynamic;
using LoDeOnline.Domain;
using LoDeOnline.Data;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using MyERP.Services;
using LinqKit;

namespace LoDeOnline.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        private const string DOMAIN_RULE_CACHE_KEY = "domain.rule.{0}-{1}-{2}";
        private readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository; // as Repository<TEntity>;
        }

        protected string UserId
        {
            get
            {
                return System.Web.HttpContext.Current.User.Identity.GetUserId();
            }
        }

        public virtual void Delete(TEntity entity)
        {
            Delete(new List<TEntity>() { entity });
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                return;
            CheckAccessRights(typeof(TEntity).Name, "Unlink");
            CheckAccessRules(entities, "Unlink");
            _repository.Delete(entities);
        }

        public TEntity GetById(object id)
        {
            return _repository.GetById(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            Insert(new List<TEntity>() { entity });
            return entity;
        }

        public virtual IEnumerable<TEntity> Insert(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                return entities;

            if (!string.IsNullOrEmpty(UserId))
            {
                foreach (var entity in entities)
                {
                    entity.CreatedById = UserId;
                    entity.WritedById = UserId;
                }
            }

            CheckAccessRights(typeof(TEntity).Name, "Create");

            _repository.Insert(entities);

            CheckAccessRules(entities, "Create");

            return entities;
        }

        private void CheckAccessRules(IEnumerable<TEntity> entities, string operation)
        {
            var irModelDataObj = DependencyResolver.Current.GetService<IRModelDataService>();
            var user_root = (ApplicationUser)irModelDataObj.GetRef("base.user_root");
            if (string.IsNullOrEmpty(UserId) || UserId == user_root.Id)
                return;

            var irRuleObj = DependencyResolver.Current.GetService<IRRuleService>();
            var domain = DomainRuleGet(typeof(TEntity).Name, mode: operation);
            if (domain == null)
                return;

            var subIds = entities.Select(x => x.Id).ToList();
            var returnedIds = Table.AsExpandable().Where(domain).Select(x => x.Id).ToList();
            _CheckRecordRulesResultCount(subIds, returnedIds, operation, description: typeof(TEntity).Name);
        }

        private void _CheckRecordRulesResultCount(IList<long> ids, IList<long> returnedIds, string operation, string description = "")
        {
            ids = ids.Distinct().ToList();
            returnedIds = returnedIds.Distinct().ToList();
            var missingIds = ids.Except(returnedIds);
            if (missingIds.Any())
            {
                throw new Exception(string.Format("Không thể hoàn tất thao tác được yêu cầu do hạn chế về bảo mật. Xin vui lòng liên hệ với quản trị hệ thống của bạn.\n\n(Document type: {0}, Operation: {1})", description, operation));
            }
        }

        private Expression<Func<TEntity, bool>> DomainRuleGet(string model_name, string mode)
        {
            var cacheManager = DependencyResolver.Current.GetService<ICacheManager>();
            var key = string.Format(DOMAIN_RULE_CACHE_KEY, UserId, model_name, mode);
            return cacheManager.Get(key, (Func<Expression<Func<TEntity, bool>>>)(() =>
            {
                var ruleObj = DependencyResolver.Current.GetService<IRRuleService>();
                var ruleIds = ruleObj.SearchRules(typeof(TEntity).Name, mode);
                if (ruleIds.Any())
                {
                    var userObj = DependencyResolver.Current.GetService<ApplicationUserManager>();
                    var groupDomains = new Dictionary<long, IList<Expression<Func<TEntity, bool>>>>();
                    var globalDomains = new List<Expression<Func<TEntity, bool>>>();
                    var user = userObj.FindById(UserId);
                    foreach (var rule in ruleObj.Table.Where(x => ruleIds.Contains(x.Id)).ToList())
                    {
                        var dom = RuleDomainGet(rule);
                        if (dom == null)
                            continue;

                        foreach (var group in rule.Groups)
                        {
                            if (user.Groups.Any(x => x.Id == group.Id))
                            {
                                if (!groupDomains.ContainsKey(group.Id))
                                    groupDomains.Add(group.Id, new List<Expression<Func<TEntity, bool>>>());
                                groupDomains[group.Id].Add(dom);
                            }
                        }
                        if (!rule.Groups.Any())
                            globalDomains.Add(dom);
                    }
                    Expression<Func<TEntity, bool>> groupDomain = x => true;
                    if (groupDomains.Any())
                    {
                        Expression<Func<TEntity, bool>> tmp = x => false;
                        foreach (var item in groupDomains.Values)
                        {
                            Expression<Func<TEntity, bool>> tmp2 = x => true;
                            foreach (var item2 in item)
                            {
                                tmp2 = tmp2.And(item2);
                            }
                            tmp = tmp.Or(tmp2);
                        }

                        groupDomain = groupDomain.And(tmp);
                    }

                    Expression<Func<TEntity, bool>> domain = x => true;
                    foreach (var d in globalDomains)
                    {
                        domain = domain.And(d);
                    }
                    domain = domain.And(groupDomain);

                    return domain;
                }

                return null;
            }));
        }

        public virtual Expression<Func<TEntity, bool>> RuleDomainGet(IRRule rule)
        {
            return null;
        }

        public void CheckAccessRights(string model, string operation)
        {
            var accessObj = DependencyResolver.Current.GetService<IRModelAccessService>();
            accessObj.Check(model, UserId, mode: operation);
        }

        public virtual void Update(TEntity entity)
        {
            Update(new List<TEntity>() { entity });
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            if (!entities.Any())
                return;

            if (!string.IsNullOrEmpty(UserId))
            {
                foreach (var entity in entities)
                {
                    entity.WritedById = UserId;
                    entity.LastUpdated = DateTime.Now;
                }
            }

            CheckAccessRights(typeof(TEntity).Name, "Write");
            CheckAccessRules(entities, "Write");

            _repository.Update(entities);
        }

        public IQueryable<TEntity> Table
        {
            get
            {
                return _repository.Table;
            }
        }

        public IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> domain = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           int offSet = 0,
           int limit = int.MaxValue)
        {
            CheckAccessRights(typeof(TEntity).Name, "Read");
            domain = _ApplyIRRules(domain, "Read");

            return _repository.Search(domain: domain, orderBy: orderBy, offSet: offSet, limit: limit);
        }

        public Expression<Func<TEntity, bool>> _ApplyIRRules(Expression<Func<TEntity, bool>> query, string mode = "Read")
        {
            var irModelDataObj = DependencyResolver.Current.GetService<IRModelDataService>();
            var user_root = (ApplicationUser)irModelDataObj.GetRef("base.user_root");
            if (string.IsNullOrEmpty(UserId) || (user_root != null && UserId == user_root.Id))
                return query;

            query = query ?? (x => true);
            var domain = DomainRuleGet(typeof(TEntity).Name, mode: mode);
            if (domain != null)
                query = query.And(domain);
            return query;
        }


        public IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters) where T : Entity, new()
        {
            return _repository.ExecuteStoredProcedureList<T>(commandText, parameters: parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            return _repository.ExecuteSqlCommand(sql, doNotEnsureTransaction: doNotEnsureTransaction,
                timeout: timeout, parameters: parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return _repository.SqlQuery<TElement>(sql, parameters);
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
