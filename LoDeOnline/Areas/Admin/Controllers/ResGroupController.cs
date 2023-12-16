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
    public class ResGroupController : Controller
    {
        private readonly ResGroupService _groupService;

        public ResGroupController(ResGroupService groupService)
        {
            _groupService = groupService;
        }

        public ActionResult List()
        {
            return PartialView();
        }

        public ActionResult Form()
        {
            return PartialView();
        }

        public ActionResult ModelAccessModal()
        {
            return PartialView();
        }
    }
}