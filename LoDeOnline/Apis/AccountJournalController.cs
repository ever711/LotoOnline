using AutoMapper.QueryableExtensions;
using LoDeOnline.Applications.Extensions;
using LoDeOnline.Data;
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
    public class AccountJournalController : ODataController
    {
        private readonly AccountJournalService _journalService;
        private readonly ApplicationUserManager _userManager;
        private readonly IUnitOfWorkAsync _unitOfWork;
        public AccountJournalController(AccountJournalService journalService,
            ApplicationUserManager userManager,
            IUnitOfWorkAsync unitOfWork)
        {
            _journalService = journalService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [EnableQuery]
        public IHttpActionResult Get()
        {
            var res = _journalService.Search().ProjectTo<AccountJournalDTO>();
            return Ok(res);
        }

        [EnableQuery]
        public IHttpActionResult Get([FromODataUri] long key)
        {
            var journal = _journalService.Search(x => x.Id == key).Include(x => x.BankAccount).FirstOrDefault();
            var res = journal.ToModel();
            res.Bank = journal.BankAccount != null && journal.BankAccount.Bank != null ? journal.BankAccount.Bank.ToModel() : null;
            return Ok(res);
        }

        public IHttpActionResult Put([FromODataUri] long key, AccountJournalDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var journal = _journalService.GetById(key);
            journal = model.ToEntity(journal);

            try
            {
                _unitOfWork.BeginTransaction();
                _journalService.Write(journal, model.BankAccNumber, model.BankId);
                _unitOfWork.Commit();

                model.Id = journal.Id;
                return Updated(model);
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

        public IHttpActionResult Post(AccountJournalDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var journal = model.ToEntity();

            try
            {
                _unitOfWork.BeginTransaction();
                _journalService.Create(journal, acc_number: model.BankAccNumber, bank_id: model.BankId);
                _unitOfWork.Commit();

                model.Id = journal.Id;
                return Created(model);
            }
            catch(Exception e)
            {
                _unitOfWork.Rollback();
                throw e;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public IHttpActionResult Delete([FromODataUri] long key)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var journal = _journalService.GetById(key);
                _journalService.Unlink(journal);
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
            var model = parameters["model"] as AccountJournalDTO;
            var user = _userManager.FindById(User.Identity.GetUserId());
            var res = new AccountJournalDTO {
                CompanyId = user.Company.Id
            };
            res.Type = model.Type;
            return Ok(res);
        }
    }
}
