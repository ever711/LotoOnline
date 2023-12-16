using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class AccountJournalController : Controller
    {
        private readonly AccountJournalService _journalService;
        public AccountJournalController(AccountJournalService journalService)
        {
            _journalService = journalService;
        }

        // GET: AccountJournal
        public ActionResult GetAccountsForBank(long bank_id)
        {
            var models = _journalService.Search(x => x.BankAccount != null && x.BankAccount.BankId == bank_id && x.Type == "bank")
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name + " - " + x.BankAccount.AccNumber
                });
            return PartialView(models);
        }
    }
}