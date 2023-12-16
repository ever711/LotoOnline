using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class LoDeCategoryController : Controller
    {
        public ActionResult List()
        {
            return PartialView();
        }

        public ActionResult Form()
        {
            return PartialView();
        }
    }
}