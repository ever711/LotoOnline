using AutoMapper.QueryableExtensions;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Data;
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
    public class LoaiDeCategoryController : ODataController
    {
        private readonly LoaiDeCategoryService _loaiDeCategoryService;
        private readonly LoDeCategoryService _loDeCategoryService;
        public LoaiDeCategoryController(LoaiDeCategoryService loaiDeCategoryService,
            LoDeCategoryService loDeCategoryService)
        {
            _loaiDeCategoryService = loaiDeCategoryService;
            _loDeCategoryService = loDeCategoryService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _loaiDeCategoryService.Search().ProjectTo<LoaiDeCategoryDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var categ = _loaiDeCategoryService.Search(x => x.Id == key).Include(x => x.LoDeCategories).FirstOrDefault();
            var res = categ.ToModel();
            res.LoDeCategories = categ.LoDeCategories.Select(x => x.ToModel());
            return Ok(res);
        }
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] long key, LoaiDeCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categ = _loaiDeCategoryService.GetById(key);
            categ = model.ToEntity(categ);
            categ.LoDeCategories.Clear();
            if (model.LoDeCategories.Any())
            {
                var loDeCategorieIds = model.LoDeCategories.Select(x => x.Id);
                categ.LoDeCategories = _loDeCategoryService.Search(x => loDeCategorieIds.Contains(x.Id)).ToList();
            }
            _loaiDeCategoryService.Update(categ);

            return Updated(model);
        }

        public IHttpActionResult Post(LoaiDeCategoryDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categ = model.ToEntity();
            if (model.LoDeCategories.Any())
            {
                var loDeCategorieIds = model.LoDeCategories.Select(x => x.Id);
                categ.LoDeCategories = _loDeCategoryService.Search(x => loDeCategorieIds.Contains(x.Id)).ToList();
            }
            _loaiDeCategoryService.Insert(categ);

            model.Id = categ.Id;
            return Created(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var categ = _loaiDeCategoryService.GetById(key);
            _loaiDeCategoryService.Delete(categ);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var res = new LoaiDeCategoryDTO();
            return Ok(res);
        }
    }
}
