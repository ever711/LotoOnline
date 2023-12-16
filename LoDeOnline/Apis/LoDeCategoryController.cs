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
    public class LoDeCategoryController : ODataController
    {
        private readonly LoDeCategoryService _loDeCategoryService;
        public LoDeCategoryController(LoDeCategoryService loDeCategoryService)
        {
            _loDeCategoryService = loDeCategoryService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _loDeCategoryService.Search().ProjectTo<LoDeCategoryDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var categ = _loDeCategoryService.Search(x => x.Id == key).FirstOrDefault();
            var res = categ.ToModel();
            return Ok(res);
        }

        [HttpPut]
        public IHttpActionResult Put([FromODataUri] long key, LoDeCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categ = _loDeCategoryService.GetById(key);
            categ = model.ToEntity(categ);
            _loDeCategoryService.Write(categ);

            return Updated(model);
        }

        public IHttpActionResult Post(LoDeCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categ = model.ToEntity();
            _loDeCategoryService.Create(categ);

            model.Id = categ.Id;
            return Created(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var categ = _loDeCategoryService.GetById(key);
            _loDeCategoryService.Delete(categ);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var res = new LoDeCategoryDTO();
            return Ok(res);
        }
    }
}
