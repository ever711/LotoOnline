using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class AccountJournalController : Controller
    {
        public ActionResult BankList()
        {
            return PartialView();
        }

        public ActionResult BankForm()
        {
            return PartialView();
        }
    }
}