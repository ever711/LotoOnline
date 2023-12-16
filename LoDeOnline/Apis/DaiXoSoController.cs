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
    public class DaiXoSoController : ODataController
    {
        private readonly DaiXoSoService _daiXSService;
        private readonly DaiXoSoRuleService _daixsRuleService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public DaiXoSoController(DaiXoSoService daiXSService,
            IUnitOfWorkAsync unitOfWork,
            DaiXoSoRuleService daixsRuleService)
        {
            _daiXSService = daiXSService;
            _unitOfWork = unitOfWork;
            _daixsRuleService = daixsRuleService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _daiXSService.Search().ProjectTo<DaiXoSoDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var daixs = _daiXSService.Search(x => x.Id == key).Include(x => x.Mien).FirstOrDefault();
            var res = daixs.ToModel();
            res.Mien = daixs.Mien?.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, DaiXoSoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();
            var daixs = _daiXSService.GetById(key);
            daixs = model.ToEntity(daixs);
            _daiXSService.Update(daixs);

            SaveRules(daixs, model.Rules);
            _unitOfWork.Commit();

            return Updated(model);
        }

        public IHttpActionResult Post(DaiXoSoDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();
            var daixs = model.ToEntity();
            _daiXSService.Insert(daixs);

            SaveRules(daixs, model.Rules);
            _unitOfWork.Commit();

            model.Id = daixs.Id;
            return Created(model);
        }

        private void SaveRules(DaiXoSo daixs, IEnumerable<DaiXoSoRuleDTO> models)
        {
            var existItems = daixs.Rules.ToList();
            var itemToRemoves = new List<DaiXoSoRule>();
            foreach (var existItem in existItems)
            {
                bool found = false;
                foreach (var item in models)
                {
                    if (item.Id == existItem.Id)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    itemToRemoves.Add(existItem);
            }

            _daixsRuleService.Delete(itemToRemoves);

            var itemsToInsert = new List<DaiXoSoRule>();
            var itemsToUpdate = new List<DaiXoSoRule>();
            foreach (var item in models)
            {
                if (item.Id == 0) //thêm mới
                {
                    var line = item.ToEntity();
                    line.DaiId = daixs.Id;
                    itemsToInsert.Add(line);
                }
                else
                {
                    var line = _daixsRuleService.GetById(item.Id);
                    line = item.ToEntity(line);
                    itemsToUpdate.Add(line);
                }
            }
            _daixsRuleService.Insert(itemsToInsert);
            _daixsRuleService.Update(itemsToUpdate);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var daixs = _daiXSService.GetById(key);
            _daiXSService.Delete(daixs);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var res = new DaiXoSoDTO();
            return Ok(res);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult GetRules([FromODataUri] long key)
        {
            var res = _daixsRuleService.Search(x => x.DaiId == key).ProjectTo<DaiXoSoRuleDTO>();
            return Ok(res);
        }
    }
}
