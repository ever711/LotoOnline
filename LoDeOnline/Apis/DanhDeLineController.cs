using AutoMapper.QueryableExtensions;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using LoDeOnline.Services;
using Microsoft.AspNet.Identity;
using MyERP.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;

namespace LoDeOnline.Apis
{
    public class DanhDeLineController : ODataController
    {
        private readonly DanhDeLineService _danhDeLineService;
        private readonly LoaiDeService _loaiDeService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly ApplicationUserManager _userManager;
        public DanhDeLineController(DanhDeLineService danhDeLineService,
            IUnitOfWorkAsync unitOfWork,
            ApplicationUserManager userManager,
            LoaiDeService loaiDeService)
        {
            _danhDeLineService = danhDeLineService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _loaiDeService = loaiDeService;
        }

        [HttpPost]
        [EnableQuery]
        public IHttpActionResult OnChangeLoaiDe(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var model = parameters["model"] as DanhDeLineDTO;
            if (model.LoaiDe != null)
            {
                var loaiDe = _loaiDeService.GetById(model.LoaiDe.Id);
                model.PriceUnit = (loaiDe.ThanhToan1K ?? 0) * 1000;
                model.IsXien = loaiDe.Type == "xien" || loaiDe.Type == "xientruot";
            }
            return Ok(model);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var res = new DanhDeLineDTO
            {
                Quantity = 1,
                PriceUnit = 0
            };
            return Ok(res);
        }
    }
}
