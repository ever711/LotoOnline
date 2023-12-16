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
    public class AccountPaymentController : BaseController
    {
        private readonly AccountPaymentService _paymentService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public AccountPaymentController(AccountPaymentService paymentService,
            ApplicationUserManager userManager,
            IUnitOfWorkAsync unitOfWork)
        {
            _paymentService = paymentService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _paymentService.Search().ProjectTo<AccountPaymentDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var payment = _paymentService.Search(x => x.Id == key).FirstOrDefault();
            var res = payment.ToModel();
            return Ok(res);
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var payment = _paymentService.GetById(key);
                _paymentService.Unlink(new List<AccountPayment>() { payment });
                _unitOfWork.Commit();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpPost]
        [EnableQuery]
        public IHttpActionResult DefaultGet(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var model = parameters["model"] as AccountPaymentDTO;
            var user = _userManager.FindById(User.Identity.GetUserId());
            var res = new AccountPaymentDTO
            {
                CompanyId = user.Company.Id
            };
            return Ok(res);
        }

        [HttpPost]
        public IHttpActionResult ActionPost(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ids = parameters["ids"] as IEnumerable<long>;

            try
            {
                _unitOfWork.BeginTransaction();
                _paymentService.Post(ids);
                _unitOfWork.Commit();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        [HttpPost]
        public IHttpActionResult Unlink(ODataActionParameters parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ids = parameters["ids"] as IEnumerable<long>;

            try
            {
                _unitOfWork.BeginTransaction();
                _paymentService.Unlink(ids);
                _unitOfWork.Commit();
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
