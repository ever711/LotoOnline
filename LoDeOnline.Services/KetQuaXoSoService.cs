using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class KetQuaXoSoService : Service<KetQuaXoSo>
    {

        public KetQuaXoSoService(IRepository<KetQuaXoSo> repository)
            : base(repository)
        {
        }

        public static IDictionary<long, string> THU_DICT
        {
            get
            {
                return new Dictionary<long, string>()
                {
                    {0, "Chủ nhật" },
                    {1, "Thứ hai" },
                    {2, "Thứ ba" },
                    {3, "Thứ tư" },
                    {4, "Thứ năm" },
                    {5, "Thứ sáu" },
                    {6, "Thứ bảy" },
                };
            }
        }

        public static IDictionary<string, string> GIAI_DICT
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"0", "ĐB" },
                    {"1", "G1" },
                    {"2", "G2" },
                    {"3", "G3" },
                    {"4", "G4" },
                    {"5", "G5" },
                    {"6", "G6" },
                    {"7", "G7" },
                    {"8", "G8" },
                };
            }
        }

        public void Create(KetQuaXoSo self)
        {
            Insert(self);
            CheckConstraints(self);
        }

        private void CheckConstraints(KetQuaXoSo self)
        {
            var res = Search(x => x.DaiXSId == self.DaiXSId && x.Ngay == self.Ngay).ToList();
            if (res.Count > 1)
                throw new Exception("Không được có kết quả xổ số có cùng đài và ngày");
        }

        public void LayKQXS(DateTime date)
        {
            var daixsObj = DependencyResolver.Current.GetService<DaiXoSoService>();
            var dais = daixsObj.Search().Select(x => new DaiXoSoDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
            }).ToList();
            foreach (var dai in dais)
                daixsObj.LayKetQuaXS(dai, date);
        }
    }
}
