using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using LoDeOnline.Services;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Models;
using Microsoft.OData;
using LoDeOnline.Data;
using System.IO;
using LoDeOnline.Domain;
using MyERP.Services;
using LoDeOnline.Domain.DTOs;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace LoDeOnline.Apis
{
    public class IRModelAccessController : BaseController
    {
        private readonly IRModelAccessService _irModelAccessService;
        private readonly IRModelDataService _modelDataService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly PartnerService _partnerService;

        public IRModelAccessController(IRModelAccessService irModelAccessService,
            ApplicationUserManager userManager,
            IUnitOfWorkAsync unitOfWork,
            PartnerService partnerService,
            IRModelDataService modelDataService)
        {
            _irModelAccessService = irModelAccessService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _partnerService = partnerService;
            _modelDataService = modelDataService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _irModelAccessService.Search().ProjectTo<IRModelAccessDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var access = _irModelAccessService.Search(x => x.Id == key).Include(x => x.Model).Include(x => x.Group).FirstOrDefault();
            var res = access.ToModel();
            res.Model = access.Model.ToModel();
            res.Group = access.Group?.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, IRModelAccessDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seq = _irModelAccessService.GetById(key);
            seq = model.ToEntity(seq);
            _irModelAccessService.Update(seq);

            return Updated(model);
        }

        public IHttpActionResult Post(IRModelAccessDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var seq = model.ToEntity();

            _irModelAccessService.Insert(seq);

            return Created(model);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var seq = _irModelAccessService.GetById(key);
            _irModelAccessService.Delete(seq);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IHttpActionResult DefaultGet()
        {
            var res = new IRModelAccessDTO();
            return Ok(res);
        }
    }
}