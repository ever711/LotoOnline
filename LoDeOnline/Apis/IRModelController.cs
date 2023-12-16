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

namespace LoDeOnline.Apis
{
    public class IRModelController : BaseController
    {
        private readonly IRModelService _irModelService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly PartnerService _partnerService;
        private readonly IRModelDataService _modelDataService;

        public IRModelController(IRModelService irModelService,
            ApplicationUserManager userManager,
            IUnitOfWorkAsync unitOfWork,
            PartnerService partnerService,
            IRModelDataService modelDataService)
        {
            _irModelService = irModelService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _partnerService = partnerService;
            _modelDataService = modelDataService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _irModelService.Search().ProjectTo<IRModelDTO>();
            return Ok(res);
        }

        public IHttpActionResult Get([FromODataUri] long key)
        {
            var res = _irModelService.GetById(key).ToModel();
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, IRModelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var irModel = _irModelService.GetById(key);
            irModel = model.ToEntity(irModel);
            _irModelService.Update(irModel);

            return Updated(model);
        }

        public IHttpActionResult Post(IRModelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var irModel = model.ToEntity();

            _irModelService.Insert(irModel);

            model.Id = irModel.Id;
            return Created(model);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var irModel = _irModelService.GetById(key);
            _irModelService.Delete(irModel);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IHttpActionResult DefaultGet()
        {
            var res = new IRModelDTO();
            return Ok(res);
        }
    }
}