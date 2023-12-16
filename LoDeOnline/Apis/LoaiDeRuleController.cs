using AutoMapper.QueryableExtensions;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Data;
using LoDeOnline.Domain.DTOs;
using LoDeOnline.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;

namespace LoDeOnline.Apis
{
    public class LoaiDeRuleController : ODataController
    {
        private readonly LoaiDeRuleService _loaiDeRuleService;
        public LoaiDeRuleController(LoaiDeRuleService loaiDeRuleService)
        {
            _loaiDeRuleService = loaiDeRuleService;
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var res = new LoaiDeRuleDTO();
            return Ok(res);
        }
    }
}
