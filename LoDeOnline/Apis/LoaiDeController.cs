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
using Microsoft.OData;
using Microsoft.AspNet.Identity;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;
using LoDeOnline.Services;
using LoDeOnline.Data;
using LoDeOnline.Domain.DTOs;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Domain;

namespace LoDeOnline.Apis
{
    public class LoaiDeController : ODataController
    {
        private readonly LoaiDeService _loaiDeService;
        private readonly LoaiDeRuleService _loaiDeRuleService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public LoaiDeController(LoaiDeService loaiDeService,
            IUnitOfWorkAsync unitOfWork,
            LoaiDeRuleService loaiDeRuleService)
        {
            _loaiDeService = loaiDeService;
            _unitOfWork = unitOfWork;
            _loaiDeRuleService = loaiDeRuleService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _loaiDeService.Search().ProjectTo<LoaiDeDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var loaiDe = _loaiDeService.Search(x => x.Id == key).Include(x => x.LoDeCateg).Include(x => x.LoaiDeCateg).FirstOrDefault();
            var res = loaiDe.ToModel();
            res.LoaiDeCateg = loaiDe.LoaiDeCateg?.ToModel();
            res.LoDeCateg = loaiDe.LoDeCateg?.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, LoaiDeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var loaiDe = _loaiDeService.GetById(key);
            loaiDe = model.ToEntity(loaiDe);
            _loaiDeService.Update(loaiDe);

            SaveRules(loaiDe, model.Rules);

            _unitOfWork.Commit();


            return Updated(model);
        }

        private void SaveRules(LoaiDe loaiDe, IEnumerable<LoaiDeRuleDTO> models)
        {
            var existItems = loaiDe.Rules.ToList();
            var itemToRemoves = new List<LoaiDeRule>();
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

            _loaiDeRuleService.Delete(itemToRemoves);

            var itemsToInsert = new List<LoaiDeRule>();
            var itemsToUpdate = new List<LoaiDeRule>();
            foreach (var item in models)
            {
                if (item.Id == 0) //thêm mới
                {
                    var line = item.ToEntity();
                    line.LoaiDeId = loaiDe.Id;
                    itemsToInsert.Add(line);
                }
                else
                {
                    var line = _loaiDeRuleService.GetById(item.Id);
                    line = item.ToEntity(line);
                    itemsToUpdate.Add(line);
                }
            }
            _loaiDeRuleService.Insert(itemsToInsert);
            _loaiDeRuleService.Update(itemsToUpdate);
        }

        public IHttpActionResult Post(LoaiDeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var loaiDe = model.ToEntity();
            _loaiDeService.Insert(loaiDe);

            SaveRules(loaiDe, model.Rules);

            _unitOfWork.Commit();

            model.Id = loaiDe.Id;
            return Created(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            _unitOfWork.BeginTransaction();
            var loaiDe = _loaiDeService.GetById(key);
            _loaiDeService.Delete(loaiDe);
            _unitOfWork.Commit();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IHttpActionResult DefaultGet()
        {
            var res = new LoaiDeDTO();
            return Ok(res);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult GetRules([FromODataUri] long key)
        {
            var res = _loaiDeRuleService.Search(x => x.LoaiDeId == key).ProjectTo<LoaiDeRuleDTO>();
            return Ok(res);
        }
    }
}