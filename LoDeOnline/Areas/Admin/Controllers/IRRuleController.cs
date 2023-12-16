using AutoMapper.QueryableExtensions;
using LoDeOnline.Models;
using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    public class IRRuleController : Controller
    {
        private readonly IRRuleService _ruleService;
        public IRRuleController(IRRuleService ruleService)
        {
            _ruleService = ruleService;
        }

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