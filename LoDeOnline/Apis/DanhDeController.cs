using AutoMapper.QueryableExtensions;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using LoDeOnline.Services;
using Microsoft.AspNet.Identity;
using MyERP.Services;
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
    public class DanhDeController : BaseController
    {
        private readonly DanhDeService _danhDeService;
        private readonly CompanyService _companyService;
        private readonly DanhDeLineService _danhDeLineService;
        private readonly LoaiDeService _loaiDeService;
        private readonly DanhDeLineXienService _lineXienService;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly ApplicationUserManager _userManager;
        public DanhDeController(DanhDeService danhDeService,
            IUnitOfWorkAsync unitOfWork,
            ApplicationUserManager userManager,
            DanhDeLineService danhDeLineService,
            CompanyService companyService,
            DanhDeLineXienService lineXienService,
            LoaiDeService loaiDeService)
        {
            _danhDeService = danhDeService;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _companyService = companyService;
            _danhDeLineService = danhDeLineService;
            _lineXienService = lineXienService;
            _loaiDeService = loaiDeService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _danhDeService.Search().ProjectTo<DanhDeDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var danhDe = _danhDeService.Search(x => x.Id == key).Include(x => x.Partner).Include(x => x.Dai).FirstOrDefault();
            var res = danhDe.ToModel();
            res.Partner = danhDe.Partner.ToModel();
            res.Dai = danhDe.Dai.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, DanhDeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var danhde = _danhDeService.GetById(key);
            danhde = model.ToEntity(danhde);
            SaveLines(danhde, model.Lines);

            _danhDeService.Compute(danhde);
            _danhDeService.Update(danhde);
            _unitOfWork.Commit();

            return Updated(model);
        }

        public IHttpActionResult Post(DanhDeDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var company = _companyService.GetById(model.CompanyId);
           
            _unitOfWork.BeginTransaction();
            var danhde = model.ToEntity();
            danhde.Company = company;
            SaveLines(danhde, model.Lines);

            _danhDeService.Compute(danhde);
            _danhDeService.Insert(danhde);
            _unitOfWork.Commit();

            model.Id = danhde.Id;
            return Created(model);
        }

        private void SaveLines(DanhDe danhde, IEnumerable<DanhDeLineDTO> models)
        {
            var existItems = danhde.Lines.ToList();
            var itemToRemoves = new List<DanhDeLine>();
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

            _danhDeLineService.Delete(itemToRemoves);

            var itemsToInsert = new List<DanhDeLine>();
            var itemsToUpdate = new List<DanhDeLine>();
            foreach (var item in models)
            {
                if (item.Id == 0) //thêm mới
                {
                    var line = item.ToEntity();
                    line.DanhDe = danhde;
                    line.LoaiDe = _loaiDeService.GetById(line.LoaiDeId);
                    SaveXienLines(line, item.XienNumbers);
                    danhde.Lines.Add(line);
                }
                else
                {
                    var line = danhde.Lines.FirstOrDefault(x => x.Id == item.Id);
                    if (line != null)
                    {
                        line = item.ToEntity(line);
                        line.LoaiDe = _loaiDeService.GetById(line.LoaiDeId);
                        SaveXienLines(line, item.XienNumbers);
                    }
                }
            }
        }

        private void SaveXienLines(DanhDeLine ddline, IEnumerable<DanhDeLineXienDTO> models)
        {
            var existItems = ddline.XienNumbers.ToList();
            var itemToRemoves = new List<DanhDeLineXien>();
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

            _lineXienService.Delete(itemToRemoves);

            var itemsToInsert = new List<DanhDeLineXien>();
            var itemsToUpdate = new List<DanhDeLineXien>();
            foreach (var item in models)
            {
                if (item.Id == 0) //thêm mới
                {
                    var line = item.ToEntity();
                    line.Line = ddline;
                    ddline.XienNumbers.Add(line);
                }
                else
                {
                    var line = ddline.XienNumbers.FirstOrDefault(x => x.Id == item.Id);
                    if (line != null)
                    {
                        line = item.ToEntity(line);
                    }
                }
            }
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var daixs = _danhDeService.GetById(key);
            _danhDeService.Unlink(daixs);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult DefaultGet()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            var res = new DanhDeDTO {
                CompanyId = user.Company.Id
            };
            return Ok(res);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult GetLines([FromODataUri] long key)
        {
            var res = _danhDeLineService.Search(x => x.DanhDeId == key).Select(x => new DanhDeLineDTO {
                Id = x.Id,
                LoaiDe = new LoaiDeDTO
                {
                    Id = x.LoaiDe.Id,
                    Name = x.LoaiDe.Name,
                },
                LoaiDeId = x.LoaiDeId,
                PriceSubtotal = x.PriceSubtotal,
                PriceUnit = x.PriceUnit,
                Quantity = x.Quantity,
                SoDanh = x.SoDanh,
                IsXien = x.LoaiDe.Type == "xien" || x.LoaiDe.Type == "xientruot",
                XienNumbers = x.XienNumbers.Select(s => new DanhDeLineXienDTO {
                    Id = s.Id,
                    SoXien = s.SoXien
                })
            });
            return Ok(res);
        }

        [HttpPost]
        [EnableQuery]
        public IHttpActionResult ActionInvoiceOpen(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var ids = parameters["ids"] as IEnumerable<long>;
            _unitOfWork.BeginTransaction();
            _danhDeService.ActionInvoiceOpen(ids);
            _unitOfWork.Commit();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [EnableQuery]
        public IHttpActionResult DoKetQua(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var ids = parameters["ids"] as IEnumerable<long>;
            _unitOfWork.BeginTransaction();
            _danhDeService.DoKetQua(ids);
            _unitOfWork.Commit();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [EnableQuery]
        public IHttpActionResult DoKetQuaAll(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var ids = _danhDeService.Search(x => x.State == "open").Select(x => x.Id).ToList();
            _unitOfWork.BeginTransaction();
            _danhDeService.DoKetQua(ids);
            _unitOfWork.Commit();
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
