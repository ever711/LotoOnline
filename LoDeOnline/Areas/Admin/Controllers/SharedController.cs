using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class SharedController : BaseController
    {
        public ActionResult App()
        {
            return PartialView();
        }

        public ActionResult Header()
        {
            return PartialView();
        }

        public ActionResult Aside()
        {
            return PartialView();
        }

        public ActionResult Settings()
        {
            return PartialView();
        }

        public ActionResult Nav()
        {
            return PartialView();
        }
    }
}