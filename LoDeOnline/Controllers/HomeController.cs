using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoDeOnline.Models;

namespace LoDeOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly KetQuaXoSoService _kqxsService;
        private readonly DaiXoSoService _daixsService;
        public HomeController(KetQuaXoSoService kqxsService,
            DaiXoSoService daixsService)
        {
            _kqxsService = kqxsService;
            _daixsService = daixsService;
        }
        public ActionResult Index()
        {
            var kq = _kqxsService.Search(orderBy: x => x.OrderByDescending(s => s.Ngay).ThenByDescending(s => s.Id)).FirstOrDefault();
            var date = kq != null ? kq.Ngay : DateTime.Today;
            var thu = ((int)date.DayOfWeek).ToString();
            var dais = _daixsService.Search(x => x.Rules.Any(s => s.Thu == thu)).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            var model = new HomeIndexViewModel
            {
                Date = date,
                Dais = dais
            };

            return View(model);
        }
    }
}
