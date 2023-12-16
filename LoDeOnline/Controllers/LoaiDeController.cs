using LoDeOnline.Models;
using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class LoaiDeController : Controller
    {
        private readonly LoaiDeService _loaiDeService;
        public LoaiDeController(LoaiDeService loaiDeService)
        {
            _loaiDeService = loaiDeService;
        }

        public ActionResult Board(long categ_id, long lode_categ_id)
        {
            var lds = _loaiDeService.Search(x => x.LoaiDeCategId == categ_id && x.LoDeCategId == lode_categ_id).Select(x => new LoaiDeSimple
            {
                Id = x.Id,
                Name = x.Name,
                MaxValue = x.MaxValue,
                MinValue = x.MinValue,
                Description = x.Description,
                ThangGap = x.ThangGap,
                ThanhToan1K = x.ThanhToan1K,
                Multi = x.Type == "xien" || x.Type == "xientruot" ? 0 : 1,
                SoLuongXien = x.Type == "xien" || x.Type == "xientruot" ? x.SoLuongXien : 0
            }).ToList();

            var model = new LoaiDeBoard
            {
                LoaiDes = lds,
            };

            return PartialView(model);
        }

        public ActionResult Info(long id)
        {
            var model = _loaiDeService.Search(x => x.Id == id).Select(x => new LoaiDeSimple
            {
                Id = x.Id,
                Name = x.Name,
                MaxValue = x.MaxValue,
                MinValue = x.MinValue,
                Description = x.Description,
                ThangGap = x.ThangGap,
                ThanhToan1K = x.ThanhToan1K,
                Multi = x.Type == "xien" || x.Type == "xientruot" ? 0 : 1,
                SoLuongXien = x.Type == "xien" || x.Type == "xientruot" ? x.SoLuongXien : 0
            }).FirstOrDefault();


            return PartialView(model);
        }

    }
}