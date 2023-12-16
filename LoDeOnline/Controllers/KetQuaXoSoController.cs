using LoDeOnline.Models;
using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class KetQuaXoSoController : Controller
    {
        private readonly DaiXoSoService _daixsService;
        private readonly KetQuaXoSoService _kqxsService;

        public KetQuaXoSoController(DaiXoSoService daixsService,
            KetQuaXoSoService kqxsService)
        {
            _daixsService = daixsService;
            _kqxsService = kqxsService;
        }

        public ActionResult DoSo()
        {
            var date = DateTime.Today;
            var pdate = date.AddDays(-1);
            var dais = _daixsService.GetDaiTheoNgay(pdate).Select(x => new DaiXoSoViewModel {
                Id = x.Id,
                Name = x.Name
            });

            var model = new DoSoViewModel
            {
                Date = date,
                PreviousDate = pdate,
                Dais = dais
            };

            return PartialView(model);
        }

        public ActionResult LoadKetQua(DateTime date, long daiId)
        {
            var kq = _kqxsService.Search(x => x.Ngay == date && x.DaiXSId == daiId).FirstOrDefault();
            if (kq == null)
                return Content("Chưa có kết quả.");

            var thu = ((int)date.DayOfWeek);
            var rows = new List<KetQuaXoSoTableRow>();
          
            var group = kq.Lines.GroupBy(x => x.Giai);

            foreach(var item in group.OrderBy(x => x.Key))
            {
                rows.Add(new KetQuaXoSoTableRow
                {
                    Giai = KetQuaXoSoService.GIAI_DICT[item.Key],
                    SoTrungs = string.Join(" - ", item.Select(x => x.SoTrung))
                });
            }

            var model = new KetQuaXoSoTable
            {
                Name = string.Format("Kết quả xổ số đài {0} - {1} ngày {2}", kq.DaiXS.Name, KetQuaXoSoService.THU_DICT[thu], kq.Ngay.ToString("dd-MM-yyyy")),
                Rows = rows
            };
            return PartialView(model);
        }
    }
}
