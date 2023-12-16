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
using LoDeOnline.Domain;
using System.Data.Entity;
using System.IO;
using LoDeOnline.Data;
using MyERP.Services;
using LoDeOnline.Domain.DTOs;
using AutoMapper.QueryableExtensions;

namespace LoDeOnline.Apis
{
    public class IRRuleController : BaseController
    {
        private readonly IRRuleService _ruleService;
        private readonly CompanyService _companyService;
        private readonly ResGroupService _groupService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly IRModelDataService _modelDataService;

        public IRRuleController(IRRuleService IRRuleService,
            ApplicationUserManager userManager,
            CompanyService companyService,
            ResGroupService groupService,
            IUnitOfWorkAsync unitOfWork,
            IRModelDataService modelDataService)
        {
            _ruleService = IRRuleService;
            _userManager = userManager;
            _companyService = companyService;
            _groupService = groupService;
            _unitOfWork = unitOfWork;
            _modelDataService = modelDataService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _ruleService.Search().ProjectTo<IRRuleDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var rule = _ruleService.Search(x => x.Id == key).Include(x => x.Model).Include(x => x.Groups).FirstOrDefault();
            var res = rule.ToModel();
            res.Model = rule.Model.ToModel();
            res.Groups = rule.Groups.Select(x => x.ToModel());
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, IRRuleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rule = _ruleService.GetById(key);
            rule = model.ToEntity(rule);
            rule.Groups.Clear();
            if (model.Groups.Any())
            {
                var group_ids = model.Groups.Select(x => x.Id);
                rule.Groups = _groupService.Table.Where(x => group_ids.Contains(x.Id)).ToList();
            }
            _ruleService.Update(rule);

            return Updated(model);
        }

        public IHttpActionResult Post(IRRuleDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rule = model.ToEntity();
            if (model.Groups.Any())
            {
                var group_ids = model.Groups.Select(x => x.Id);
                rule.Groups = _groupService.Table.Where(x => group_ids.Contains(x.Id)).ToList();
            }
            _ruleService.Insert(rule);

            model = rule.ToModel();
            return Created(model);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var rule = _ruleService.GetById(key);
            _ruleService.Delete(rule);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IHttpActionResult DefaultGet()
        {
            var res = new IRRuleDTO();
            return Ok(res);
        }
    }
}