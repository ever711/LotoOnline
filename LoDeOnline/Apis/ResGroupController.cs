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
using Microsoft.AspNet.Identity;
using MyERP.Services;
using LoDeOnline.Domain.DTOs;
using AutoMapper.QueryableExtensions;
using System.Data.Entity;

namespace LoDeOnline.Apis
{
    public class ResGroupController : BaseController
    {
        private readonly ResGroupService _resGroupService;
        private readonly IRModelAccessService _modelAccessService;
        private readonly IRRuleService _irRuleService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        private readonly PartnerService _partnerService;

        public ResGroupController(ResGroupService resGroupService,
            ApplicationUserManager userManager,
            IUnitOfWorkAsync unitOfWork,
            PartnerService partnerService,
            IRModelAccessService modelAccessService,
            IRRuleService irRuleService)
        {
            _resGroupService = resGroupService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _partnerService = partnerService;
            _modelAccessService = modelAccessService;
            _irRuleService = irRuleService;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _resGroupService.Search().ProjectTo<ResGroupDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var group = _resGroupService.Search(x => x.Id == key).Include(x => x.Implieds)
                .Include(x => x.Users).Include(x => x.Rules).FirstOrDefault();
            var res = group.ToModel();
            res.Implieds = group.Implieds.Select(x => x.ToModel());
            res.Users = group.Users.Select(x => x.ToModel());
            res.Rules = group.Rules.Select(x => x.ToModel());
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, ResGroupDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();
            var group = _resGroupService.GetById(key);
            group = model.ToEntity(group);
            var implied_groups = new List<ResGroup>();
            foreach (var item in group.Implieds.ToList())
            {
                if (!model.Implieds.Any(x => x.Id == item.Id))
                {
                    group.Implieds.Remove(item);
                    implied_groups.Add(item);
                }
            }
           
            foreach (var item in model.Implieds)
            {
                var g = _resGroupService.GetById(item.Id);
                if (!group.Implieds.Contains(g))
                {
                    group.Implieds.Add(g);
                    implied_groups.Add(g);
                }
            }

            var users = new List<ApplicationUser>();
            foreach (var item in group.Users.ToList())
            {
                if (!model.Users.Any(x => x.Id == item.Id))
                {
                    group.Users.Remove(item);
                    users.Add(item);
                }
            }

            foreach (var item in model.Users)
            {
                var u = _userManager.FindById(item.Id);
                if (!group.Users.Contains(u))
                {
                    group.Users.Add(u);
                    users.Add(u);
                }
            }


            group.Rules.Clear();
            foreach (var item in model.Rules)
            {
                group.Rules.Add(_irRuleService.GetById(item.Id));
            }

            _resGroupService.Write(group, implied_ids: implied_groups, users: users);

            SaveModelAccesses(group, model.ModelAccesses);

            _unitOfWork.Commit();

            return Updated(model);
        }

        private void SaveModelAccesses(ResGroup group, IEnumerable<IRModelAccessDTO> accesses)
        {
            var existAccesses = group.ModelAccesses.ToList();
            var accessesToRemove = new List<IRModelAccess>();
            foreach (var existAccess in existAccesses)
            {
                bool found = false;
                foreach (var item in accesses)
                {
                    if (item.Id == existAccess.Id)
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    accessesToRemove.Add(existAccess);
            }

            foreach (var access in accessesToRemove.ToList())
            {
                _modelAccessService.Delete(access);
            }

            foreach (var item in accesses)
            {
                if (item.Id == 0) //thêm mới
                {
                    var access = item.ToEntity();
                    access.GroupId = group.Id;
                    _modelAccessService.Insert(access);
                }
                else
                {
                    var access = _modelAccessService.GetById(item.Id);
                    access = item.ToEntity(access);
                    _modelAccessService.Update(access);
                }
            }
        }

        public IHttpActionResult Post(ResGroupDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();
            var group = model.ToEntity();
            foreach (var item in model.Implieds)
            {
                group.Implieds.Add(_resGroupService.GetById(item.Id));
            }

            var users = new List<ApplicationUser>();
            foreach (var item in model.Users)
            {
                var user = _userManager.FindById(item.Id);
                group.Users.Add(user);
                users.Add(user);
            }

            foreach (var item in model.Rules)
            {
                group.Rules.Add(_irRuleService.GetById(item.Id));
            }

            _resGroupService.Create(group, users: users);

            foreach (var item in model.ModelAccesses)
            {
                var access = item.ToEntity();
                access.GroupId = group.Id;
                _modelAccessService.Insert(access);
            }

            _unitOfWork.Commit();

            model = group.ToModel();
            return Created(model);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            var group = _resGroupService.GetById(key);
            _resGroupService.Delete(group);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpGet]
        public IHttpActionResult DefaultGet()
        {
            var res = new ResGroupDTO();
            return Ok(res);
        }

        [HttpGet]
        [EnableQuery]
        public IHttpActionResult GetModelAccesses([FromODataUri] long key)
        {
            var res = _modelAccessService.Search(x => x.GroupId == key).Select(x => new IRModelAccessDTO {
                Active = x.Active,
                Name = x.Name,
                PermRead = x.PermRead,
                PermCreate = x.PermCreate,
                PermUnlink = x.PermUnlink,
                PermWrite = x.PermWrite,
                ModelId = x.ModelId,
                Model = new IRModelDTO
                {
                    Id = x.Model.Id,
                    Name = x.Model.Name,
                }
            });
            return Ok(res);
        }
    }
}