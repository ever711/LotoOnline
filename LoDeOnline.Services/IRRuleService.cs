using LoDeOnline.Data;
using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LinqKit;
using MyERP.Services;

namespace LoDeOnline.Services
{
    public class IRRuleService : Service<IRRule>
    {
        public string[] _MODES = new string[] { "Read", "Write", "Create", "Unlink" };
        private readonly IRepository<IRModel> _modelRepository;
        public IRRuleService(IRepository<IRRule> ruleRepository,
            IRepository<IRModel> modelRepository)
            : base(ruleRepository)
        {
            _modelRepository = modelRepository;
        }

        public void Check(string createUId, string writeUId, string mode)
        {
            if (string.IsNullOrEmpty(createUId))
                return;

            var modes = new string[] { "read", "write", "create", "unlink" };
            if (!modes.Contains(mode))
                throw new Exception("Invalid access mode");

            //We check if a specific rule exists
            var rule = Search(domain: x => x.Groups.SelectMany(s => s.Users).Any(s => s.Id == writeUId) && x.Active == true).FirstOrDefault();

            if (rule == null)
                return;


            //Tạm thời tạo 1 rule duy nhất: chỉ được chỉnh sửa dữ liệu cá nhân, nếu nhóm nào có rule này
            // thì sẽ được áp dụng, chừng sau mở rộng lưu forcedomain vào trong database
            if (createUId != writeUId)
                throw new Exception("Bạn chỉ được thay đổi dữ liệu của chính mình.");

            bool res = false;
            switch (mode)
            {
                case "read":
                    res = rule.PermRead ?? false;
                    break;
                case "write":
                    res = rule.PermWrite ?? false;
                    break;
                case "create":
                    res = rule.PermCreate ?? false;
                    break;
                case "unlink":
                    res = rule.PermUnlink ?? false;
                    break;
                default:
                    res = true;
                    break;
            }

            if (!res)
                throw new Exception("Bạn không được phép thực hiện thao tác này.");
        }

        public Expression<Func<TEntity, bool>> DomainGet<TEntity>(string modelName, string mode = "Read") where TEntity : Entity
        {
            return null;
            //var dom = _ComputeDomain<TEntity>(modelName, mode);
            //return dom;
        }

        private Expression<Func<TEntity, bool>> _ComputeDomain<TEntity>(string modelName, string mode = "Read") where TEntity : Entity
        {
            if (!_MODES.Contains(mode))
                throw new Exception("Invalid mode: " + mode);

            //if SUPERUSER_ID return None

            var rule_ids = SqlQuery<long>("SELECT r.Id " +
                "FROM IRRules r " +
                "INNER JOIN IRModels m ON r.ModelId=m.Id " +
                "WHERE m.Model=@modelName " +
                "AND r.Active=1 " +
                "AND r.Perm" + mode + "=1 " +
                "AND (r.Id in (SELECT RuleGroupId FROM RuleGroupRel gRel " +
                "JOIN ResGroupsUsersRel uRel ON gRel.GroupId=uRel.GId " +
                "WHERE uRel.UId=@uid) OR r.Global=1)",
                new object[] { new SqlParameter("modelName", modelName), new SqlParameter("uid", UserId) }).ToList();

            if (!rule_ids.Any())
                return x => true;

            if (rule_ids.Any())
            {
                var userObj = DependencyResolver.Current.GetService<ApplicationUserManager>();
                var user = userObj.FindById(UserId);
                var groupDomains = new Dictionary<long, IList<Expression<Func<TEntity, bool>>>>();
                var globalDomains = new List<Expression<Func<TEntity, bool>>>();
                var rules = Search(x => rule_ids.Contains(x.Id)).ToList();
                foreach (var rule in rules)
                {
                    var ruleDomain = rule.DomainForce;
                    var dom = System.Linq.Dynamic.DynamicExpression.ParseLambda<TEntity, bool>(ruleDomain);
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
                var groupDomain = new List<Expression<Func<TEntity, bool>>>();
                if (groupDomains.Any())
                {
                    foreach (var item in groupDomains.Values)
                    {
                        foreach (var item2 in item)
                        {
                            groupDomain.Add(item2);
                        }
                    }
                }
                Expression<Func<TEntity, bool>> domain = x => true;
                foreach (var d in globalDomains.Concat(groupDomain))
                {
                    domain.And(d);
                }
                return domain;
            }

            return null;
        }

        public IList<long> SearchRules(string modelName, string mode)
        {
            if (!_MODES.Contains(mode))
                throw new Exception("Invalid mode: " + mode);

            var rules = SqlQuery<long>("SELECT r.Id " +
                "FROM IRRules r " +
                "INNER JOIN IRModels m ON r.ModelId=m.Id " +
                "WHERE m.Model=@modelName " +
                "AND r.Active=1 " +
                "AND r.Perm" + mode + "=1 " +
                "AND (r.Id in (SELECT RuleGroupId FROM RuleGroupRel gRel " +
                "JOIN ResGroupsUsersRel uRel ON gRel.GroupId=uRel.GId " +
                "WHERE uRel.UId=@uid) OR r.Global=1)",
                new object[] { new SqlParameter("modelName", modelName), new SqlParameter("uid", UserId) }).ToList();
            return rules;
        }
    }
}
