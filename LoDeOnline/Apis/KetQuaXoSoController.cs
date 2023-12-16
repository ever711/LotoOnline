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
    public class KetQuaXoSoController : ODataController
    {
        private readonly KetQuaXoSoService _kqxsService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly KetQuaXoSoCTService _kqxsctService;
        public KetQuaXoSoController(KetQuaXoSoService kqxsService,
            IUnitOfWorkAsync unitOfWork,
            KetQuaXoSoCTService kqxsctService)
        {
            _kqxsService = kqxsService;
            _unitOfWork = unitOfWork;
            _kqxsctService = kqxsctService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _kqxsService.Search().ProjectTo<KetQuaXoSoDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var kqxs = _kqxsService.Search(x => x.Id == key).Include(x => x.DaiXS).FirstOrDefault();
            var res = kqxs.ToModel();
            res.DaiXS = kqxs.DaiXS.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var daixs = _kqxsService.GetById(key);
            _kqxsService.Delete(daixs);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IHttpActionResult GetLines([FromODataUri] long key)
        {
            var res = _kqxsctService.Search(x => x.KQXSId == key).ProjectTo<KetQuaXoSoCTDTO>();
            return Ok(res);
        }

        //[HttpGet]
        //public IHttpActionResult LayKetQua(DateTime? date = null)
        //{
        //    var d = date ?? DateTime.Today;
        //    _unitOfWork.BeginTransaction();
        //    _kqxsService.LayKQXS(d);
        //    _unitOfWork.Commit();

        //    return Ok(true);
        //}

        [HttpPost]
        public IHttpActionResult LayKetQua(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var date = parameters.ContainsKey("date") ? DateTime.Parse(parameters["date"].ToString()) : DateTime.Today;
            _unitOfWork.BeginTransaction();
            _kqxsService.LayKQXS(date);
            _unitOfWork.Commit();

            return Ok(true);
        }
    }
}
