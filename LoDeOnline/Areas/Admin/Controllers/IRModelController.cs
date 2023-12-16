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
    public class IRModelController : Controller
    {
        private readonly IRModelService _modelService;

        public IRModelController(IRModelService modelService)
        {
            _modelService = modelService;
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