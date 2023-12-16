using LoDeOnline.Models;
using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class LoDeCategoryController : Controller
    {
        private readonly LoDeCategoryService _loDeCategoryService;
        private readonly LoaiDeCategoryService _loaiDeCategoryService;
        private readonly DaiXoSoService _daixsService;
        private readonly LoaiDeService _loaiDeService;
        public LoDeCategoryController(LoDeCategoryService loDeCategoryService,
            LoaiDeService loaiDeService,
            DaiXoSoService daixsService,
            LoaiDeCategoryService loaiDeCategoryService)
        {
            _loDeCategoryService = loDeCategoryService;
            _loaiDeService = loaiDeService;
            _daixsService = daixsService;
            _loaiDeCategoryService = loaiDeCategoryService;
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var models = _loDeCategoryService.Search().Select(x => new LoDeCategoryTopMenu
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
            });
            return PartialView(models);
        }

        public ActionResult Show(long id)
        {
            var categs = _loaiDeCategoryService.Search(x => x.LoDeCategories.Any(s => s.Id == id), orderBy: x => x.OrderBy(s => s.Sequence)).Select(x => new LoaiDeCategorySimple
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var date = DateTime.Today;
            var thu = ((int)date.DayOfWeek).ToString();
            var dais = _daixsService.Search(x => x.Rules.Any(s => s.Thu == thu) && x.MienId == id).Select(x => new DaiXoSoViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            var model = new LoDeCategoryShow
            {
                Id = id,
                LoaiDeCategs = categs,
                Dais = dais,
                Date = date,
            };
            return View(model);
        }
        public ActionResult Info(long id)
        {
            var categs = _loaiDeCategoryService.Search(x => x.LoDeCategories.Any(s => s.Id == id), orderBy: x => x.OrderBy(s => s.Sequence)).Select(x => new LoaiDeCategorySimple
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();

            var date = DateTime.Today;
            var thu = ((int)date.DayOfWeek).ToString();
            var dais = _daixsService.Search(x => x.Rules.Any(s => s.Thu == thu) && x.MienId == id).Select(x => new DaiXoSoViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            var model = new LoDeCategoryShow
            {
                Id = id,
                LoaiDeCategs = categs,
                Dais = dais,
                Date = date,
            };
            return View(model);
        }

        public ActionResult GetValues(string day)
        {
            return Json(true);
        }
    }
}