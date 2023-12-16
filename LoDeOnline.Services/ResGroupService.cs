using Microsoft.AspNet.Identity;
using LoDeOnline.Data;
using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyERP.Services;
using System.Data.SqlClient;

namespace LoDeOnline.Services
{
    public class ResGroupService : Service<ResGroup>
    {
        private readonly ICacheManager _cacheManager;
        public ResGroupService(IRepository<ResGroup> resGroupRepository,
            ICacheManager cacheManager)
            : base(resGroupRepository)
        {
            _cacheManager = cacheManager;
        }

        public bool HasGroup(string group_ext_id)
        {
            var uid = UserId;
            if (string.IsNullOrEmpty(uid))
                return false;
            var modelDataObj = DependencyResolver.Current.GetService<IRModelDataService>();
            var user_root = (ApplicationUser)modelDataObj.GetRef("base.user_root");
            if (user_root != null && user_root.Id == uid)
                return true;
            return _HasGroup(uid, group_ext_id);
        }

        public bool _HasGroup(string uid, string group_ext_id)
        {
            var group_ext_split = group_ext_id.Split('.');
            var module = group_ext_split[0];
            var ext_id = group_ext_split[1];
            var res = SqlQuery<int?>("SELECT 1 FROM ResGroupsUsersRel WHERE UId=@uid AND GId IN " +
                            "(SELECT ResId FROM IRModelDatas WHERE Module = @module AND Name = @name)", new object[] {
                                new SqlParameter("@uid", uid),
                                 new SqlParameter("@module", module),
                                  new SqlParameter("@name", ext_id),
                            }).ToList().FirstOrDefault();

            return res.HasValue && res.Value == 1;
        }

        public void Create(ResGroup self, IEnumerable<ApplicationUser> users = null)
        {
            Insert(self);
            if (users != null)
            {
                foreach (var user in users)
                {
                    if (!self.Users.Contains(user))
                        self.Users.Add(user);
                }
                Write(self, users: users);
            }
            ClearCache(self);
        }

        public void Write(ResGroup self, IEnumerable<ResGroup> implied_ids = null,
            IEnumerable<ApplicationUser> users = null)
        {
            Write(new List<ResGroup>() { self }, implied_ids: implied_ids, users: users);
            ClearCache(self);
        }

        private void ClearCache(ResGroup self)
        {
        }

        private void ClearCache(IEnumerable<ResGroup> self)
        {
        }

        public void Write(IEnumerable<ResGroup> self, IEnumerable<ResGroup> implied_ids = null,
            IEnumerable<ApplicationUser> users = null)
        {
            Update(self);
            if ((users != null && users.Any()) || (implied_ids != null && implied_ids.Any()))
            {
                foreach (var group in self)
                {
                    var gs = _GetTransImplied(new List<ResGroup>() { group })[group.Id];
                    var usrs = group.Users.ToList();
                    foreach (var g in gs)
                    {
                        foreach (var u in usrs)
                        {
                            if (!g.Users.Contains(u))
                                g.Users.Add(u);
                        }
                        Update(g);
                    }
                }
            }
            ClearCache(self);
        }

        public IDictionary<long, IList<ResGroup>> _GetTransImplied(IList<ResGroup> groups)
        {
            var memo = new Dictionary<long, IList<ResGroup>>();
            var res = new Dictionary<long, IList<ResGroup>>();

            IList<ResGroup> ComputedSet(ResGroup g)
            {
                if (!memo.ContainsKey(g.Id))
                {
                    memo.Add(g.Id, g.Implieds.ToList());
                    foreach (var h in g.Implieds)
                    {
                        foreach (var s in ComputedSet(h))
                        {
                            if (!memo[g.Id].Contains(s))
                                memo[g.Id].Add(s);
                        }
                    }
                }
                return memo[g.Id];
            }

            foreach (var g in groups)
            {
                if (!res.ContainsKey(g.Id))
                    res.Add(g.Id, new List<ResGroup>());
                res[g.Id] = ComputedSet(g);
            }

            return res;
        }

        public IList<GetGroupsByApplicationRes> GetGroupsByApplication(string userId)
        {
            var gIds = GetApplicationGroups();
            var byApp = new Dictionary<long, IList<ResGroup>>();
            foreach (var g in gIds)
            {
                if (g.Category == null || g.Category.Visible == false)
                    continue;
                if (!byApp.ContainsKey(g.Category.Id))
                    byApp.Add(g.Category.Id, new List<ResGroup>());
                byApp[g.Category.Id].Add(g);
            }

            var res = new List<GetGroupsByApplicationRes>();
            foreach (var app in byApp)
            {
                var groupList = byApp[app.Key];
                if (!groupList.Any())
                    continue;
                var g = groupList[0];
                var gs = Linearized(groupList);
                if (gs != null)
                {
                    var selectedGroup = FindSelectedGroup(userId, gs);
                    res.Add(new GetGroupsByApplicationRes
                    {
                        CategoryId = g.Category.Id,
                        CategoryName = g.Category.Name,
                        Selections = gs.Select(x => new GroupSelection
                        {
                            Id = x.Id,
                            Name = x.Name,
                        }),
                        SelectedGroupId = selectedGroup != null ? selectedGroup.Id : (long?)null,
                        SelectedGroupName = selectedGroup != null ? selectedGroup.Name : string.Empty,
                    });
                }
            }

            return res;
        }

        private ResGroup FindSelectedGroup(string uid, IList<ResGroup> gs)
        {
            if (string.IsNullOrEmpty(uid))
                return null;
            var userObj = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var user = userObj.FindById(uid);
            ResGroup res = null;
            foreach (var g in gs)
            {
                var group = user.Groups.FirstOrDefault(x => x.Id == g.Id);
                if (group == null)
                    continue;
                if (res == null)
                {
                    res = group;
                    continue;
                }

                if (_GetTransImplied(new List<ResGroup>() { group })[group.Id].Any(x => x.Id == res.Id))
                    res = group;
            }

            return res;
        }

        private IList<ResGroup> Linearized(IList<ResGroup> gs)
        {
            var order = gs.ToDictionary(x => x.Id, x => 0);
            foreach (var g in gs)
            {
                var implieds = _GetTransImplied(new List<ResGroup>() { g })[g.Id];
                foreach (var h in gs.Intersect(implieds))
                {
                    order[h.Id] -= 1;
                }
            }

            if (order.Count() == gs.Count)
                return gs.OrderBy(x => order[x.Id]).ToList();
            return null;
        }

        private IList<ResGroup> GetApplicationGroups()
        {
            return Search().ToList();
        }



    }

    public class GetGroupsByApplicationRes
    {
        public GetGroupsByApplicationRes()
        {
            Selections = new List<GroupSelection>();
        }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<GroupSelection> Selections { get; set; }
        public long? SelectedGroupId { get; set; }
        public string SelectedGroupName { get; set; }
    }

    public class GroupSelection
    {
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
