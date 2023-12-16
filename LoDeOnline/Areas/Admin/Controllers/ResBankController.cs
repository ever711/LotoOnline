using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class ResBankController : Controller
    {
        public ActionResult FormModal()
        {
            return PartialView();
        }
    }
}