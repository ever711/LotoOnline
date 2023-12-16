using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class AccountPaymentController : Controller
    {
        public ActionResult NapTien()
        {
            return PartialView();
        }

        public ActionResult RutTien()
        {
            return PartialView();
        }
    }
}