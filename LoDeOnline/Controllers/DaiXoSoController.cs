using LoDeOnline.Models;
using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class DaiXoSoController : Controller
    {
        private readonly DaiXoSoService _daixsService;
        public DaiXoSoController(DaiXoSoService daixsService)
        {
            _daixsService = daixsService;
        }

        [HttpPost]
        public ActionResult LoadDaiTheoMienVaNgay(DateTime date, long categ_id)
        {
            var thu = ((int)date.DayOfWeek).ToString();
            var models = _daixsService.Search(x => x.Rules.Any(s => s.Thu == thu) && x.MienId == categ_id).Select(x => new DaiXoSoViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return PartialView(models);
        }

        public ActionResult LoadDaiTheoNgay(DateTime date)
        {
            var thu = ((int)date.DayOfWeek).ToString();
            var models = _daixsService.Search(x => x.Rules.Any(s => s.Thu == thu)).Select(x => new DaiXoSoViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return PartialView(models);
        }
    }
}