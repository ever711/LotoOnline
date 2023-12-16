using AutoMapper.QueryableExtensions;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using LoDeOnline.Services;
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
    public class PartnerController : ODataController
    {
        private readonly PartnerService _partnerService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public PartnerController(PartnerService partnerService,
            IUnitOfWorkAsync unitOfWork)
        {
            _partnerService = partnerService;
            _unitOfWork = unitOfWork;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _partnerService.Search().ProjectTo<PartnerDTO>();
            return Ok(res);
        }
    }
}
