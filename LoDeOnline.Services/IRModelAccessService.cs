using LoDeOnline.Data;
using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.SqlClient;
using MyERP.Services;

namespace LoDeOnline.Services
{
    public class IRModelAccessService : Service<IRModelAccess>
    {
        private const string CACHE_KEY = "ir.model.access-{0}-{1}-{2}";
        private const string PATTERN_KEY = "ir.model.access";
        private readonly ICacheManager _cacheManager;
        public string[] TransientModels = new string[] {
            "StockReturnPicking",
            "StockReturnPickingLine",
            "AccountReportGeneralLedger",
            "AccountPartnerLedger",
            "AccountInvoiceRefund",
            "StockSettings",
            "PosConfigSetting",
            "SaleSettings",
            "BaseConfigSettings",
            "PurchaseSettings",
            "CashBoxOut",
            "CashBoxIn",
            "PosMakePayment",
            "StockChangeProductQty",
            "HCMPTOrder",
            "AccountRegisterPayment"
        };

        public IRModelAccessService(IRepository<IRModelAccess> modelAccessRepository,
            ICacheManager cacheManager)
            : base(modelAccessRepository)
        {
            _cacheManager = cacheManager;
        }

        public bool Check(string model, string uid, string mode = "Read", bool raiseException = true)
        {
            var key = string.Format(CACHE_KEY, UserId, model, mode);
            var res = _cacheManager.Get(key, () =>
            {
                var irModelDataObj = DependencyResolver.Current.GetService<IRModelDataService>();
                var user_root = (ApplicationUser)irModelDataObj.GetRef("base.user_root");
                if (string.IsNullOrEmpty(UserId) || UserId == user_root.Id)
                    return true;

                var modes = new string[] { "Read", "Write", "Create", "Unlink" };
                if (!modes.Contains(mode))
                    throw new Exception("Invalid access mode");

                //transient models no nead check access right
                if (TransientModels.Contains(model))
                    return true;

                //We check if a specific rule exists
                var r = SqlQuery<int?>("SELECT ISNULL(MAX(CASE WHEN Perm" + mode + " = 1 THEN 1 ELSE 0 END),0)" +
                   "FROM IRModelAccesses a " +
                   "INNER JOIN IRModels m ON m.Id=a.ModelId " +
                   "INNER JOIN ResGroupsUsersRel gu ON gu.GId=a.GroupId " +
                   "WHERE m.Model=@modelName " +
                   "AND gu.UId=@uid " +
                   "AND a.Active=1",
                   new object[] { new SqlParameter("modelName", model), new SqlParameter("uid", UserId) }).ToList().FirstOrDefault();
                if (!r.HasValue || r == 0)
                {
                    //there is no specific rule. We check the generic rule
                    r = SqlQuery<int?>("SELECT MAX(CASE WHEN Perm" + mode + " = 1 THEN 1 ELSE 0 END)" +
                   "FROM IRModelAccesses a " +
                   "INNER JOIN IRModels m ON m.Id=a.ModelId " +
                   "WHERE m.Model=@modelName " +
                   "AND a.GroupId IS NULL " +
                   "AND a.Active=1",
                   new object[] { new SqlParameter("modelName", model), new SqlParameter("uid", UserId) }).ToList().FirstOrDefault();
                }

                return Convert.ToBoolean(r ?? 0);
            });


            if (!res && raiseException)
            {
                var groups = string.Join("\n\t", groupNamesWithAccess(model, mode).Select(x => "- " + x));
                var msgHeads = new Dictionary<string, string>();
                msgHeads.Add("Read", "Xin lỗi, bạn không được phép xem tài liệu này.");
                msgHeads.Add("Write", "Xin lỗi, bạn không được phép chỉnh sửa tài liệu này.");
                msgHeads.Add("Create", "Xin lỗi, bạn không được phép tạo loại tài liệu này.");
                msgHeads.Add("Unlink", "Xin lỗi, bạn không được phép xóa tài liệu này.");
                string msgTail = "";
                if (groups.Any())
                {
                    msgTail = string.Format("Chỉ những người dùng thuộc mức truy cập sau được quyền thao tác: \n{0}.\n\n model: {1}", groups, model);
                }
                else
                {
                    msgTail = string.Format("Vui lòng liên hệ admin nếu bạn nghĩ đây là lỗi. model: {0}", model);
                }

                var msg = msgHeads[mode] + " " + msgTail;
                throw new Exception(msg);
            }

            return res;
        }

        public IList<string> groupNamesWithAccess(string model, string mode)
        {
            var accessModes = new string[] { "Read", "Write", "Create", "Unlink" };
            if (!accessModes.Contains(mode))
                throw new Exception("Invalid access mode: " + mode);
            var list = SqlQuery<GroupNamesWithAccessRes>("SELECT c.Name AS CategoryName,g.Name AS GroupName " +
               "FROM IRModelAccesses a " +
               "INNER JOIN IRModels m ON m.Id=a.ModelId " +
               "INNER JOIN ResGroups g ON a.GroupId=g.Id " +
               "LEFT JOIN IRModuleCategories c ON c.Id=g.CategoryId " +
               "WHERE m.Model=@modelName " +
               "AND a.Perm" + mode + "=1" +
               "AND a.Active=1",
               new object[] { new SqlParameter("modelName", model) }).ToList();
            return list.Select(x => string.Format("{0}/{1}", x.CategoryName, x.GroupName)).ToList();
        }

        public override IEnumerable<IRModelAccess> Insert(IEnumerable<IRModelAccess> entities)
        {
            _cacheManager.Clear();
            return base.Insert(entities);
        }

        public override void Update(IEnumerable<IRModelAccess> entities)
        {
            _cacheManager.Clear();
            base.Update(entities);
        }

        public override void Delete(IEnumerable<IRModelAccess> entities)
        {
            _cacheManager.Clear();
            base.Delete(entities);
        }

        public bool CheckGroups(string group)
        {
            //Check whether the current user has the given group.
            var grouparr = group.Split('.');
            if (grouparr.Length != 2)
                return false;
            var res = SqlQuery<int>("SELECT 1 FROM ResGroupsUsersRel " +
                            "WHERE UId=@uid AND gid IN( " +
                                "SELECT ResId FROM IRModelDatas WHERE Module=@module AND Name=@name)",
                                new SqlParameter("uid", UserId),
                                new SqlParameter("module", grouparr[0]),
                                new SqlParameter("name", grouparr[1])).ToList();
            return res.Any();
        }

        public class GroupNamesWithAccessRes
        {
            public string CategoryName { get; set; }

            public string GroupName { get; set; }
        }
    }
}
