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
    public class ResBankController : ODataController
    {
        private readonly ResBankService _bankService;
        public ResBankController(ResBankService bankService)
        {
            _bankService = bankService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _bankService.Search().ProjectTo<ResBankDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var bank = _bankService.Search(x => x.Id == key).FirstOrDefault();
            var res = bank.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, ResBankDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bank = _bankService.GetById(key);
            bank = model.ToEntity(bank);
            _bankService.Update(bank);

            return Updated(model);
        }

        public IHttpActionResult Post(ResBankDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bank = model.ToEntity();
            _bankService.Insert(bank);

            model.Id = bank.Id;
            return Created(model);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var bank = _bankService.GetById(key);
            _bankService.Delete(bank);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var res = new ResBankDTO();
            return Ok(res);
        }
    }
}
