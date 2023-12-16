using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using Microsoft.AspNet.Identity;
using MyERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class DanhDeLineService : Service<DanhDeLine>
    {
        public DanhDeLineService(IRepository<DanhDeLine> repository)
            : base(repository)
        {
        }

        public void Compute(IEnumerable<DanhDeLine> self)
        {
            _ComputeSoDanh(self);
            foreach(var item in self)
            {
                if (item.DanhDe != null)
                {
                    item.DaiId = item.DanhDe.DaiId;
                }
                var currency = item.DanhDe.Company.Currency;
                item.PriceSubtotal = currency.Round(item.PriceUnit * item.Quantity);
            }
            _CheckConstraints(self);
        }

        private void _CheckConstraints(IEnumerable<DanhDeLine> self)
        {
            foreach(var line in self)
            {
                var so_danhs = line.LoaiDe.Type == "xien" || line.LoaiDe.Type == "xientruot" ? line.XienNumbers.Select(x => x.SoXien) :
                    new List<string>() { line.SoDanh };
                var soluongxien = line.LoaiDe.SoLuongXien ?? 0;
                if ((line.LoaiDe.Type == "xien" || line.LoaiDe.Type == "xientruot") && so_danhs.Count() != (line.LoaiDe.SoLuongXien ?? 0))
                    throw new Exception(string.Format("Bạn phải nhập đúng {0} số xiên", soluongxien));
                foreach(var sodanh in so_danhs)
                {
                    int n;
                    if (!int.TryParse(sodanh, out n))
                        throw new Exception("Số đánh không hợp lệ");
                    if ((line.LoaiDe.MinValue.HasValue && n < line.LoaiDe.MinValue) ||
                        (line.LoaiDe.MaxValue.HasValue && n > line.LoaiDe.MaxValue))
                        throw new Exception(string.Format("Số đánh phải từ {0} đến {1}", line.LoaiDe.MinValue, line.LoaiDe.MaxValue));
                }
            }
        }

        private void _ComputeSoDanh(IEnumerable<DanhDeLine> self)
        {
            foreach(var line in self)
            {
                if (line.LoaiDe.Type == "xien" || line.LoaiDe.Type == "xientruot")
                    line.SoDanh = string.Join(" - ", line.XienNumbers.Select(x => x.SoXien));
            }
        }

        public override Expression<Func<DanhDeLine, bool>> RuleDomainGet(IRRule rule)
        {
            var userObj = DependencyResolver.Current.GetService<ApplicationUserManager>();
            var user = userObj.FindById(UserId);
            switch (rule.Code)
            {
                case "sale.danh_de_line_personal_rule":
                    return x => x.DanhDe.PartnerId == user.PartnerId;
                case "sale.danh_de_line_see_all":
                    return x => true;
                default:
                    return null;
            }
        }
    }
}
