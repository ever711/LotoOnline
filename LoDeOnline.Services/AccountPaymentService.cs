using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class AccountPaymentService : Service<AccountPayment>
    {
        public AccountPaymentService(IRepository<AccountPayment> repository)
            : base(repository)
        {
        }

        public void Unlink(IEnumerable<AccountPayment> self)
        {
            foreach (var rec in self)
            {
                if (rec.State != "draft")
                    throw new Exception("Bạn không thể xóa thanh toán đã được vào sổ.");
            }

            Delete(self);
        }

        public void Unlink(IEnumerable<long> ids)
        {
            var payments = Search(x => ids.Contains(x.Id)).ToList();
            Unlink(payments);
        }

        public void Post(IEnumerable<long> ids)
        {
            var payments = Search(x => ids.Contains(x.Id)).ToList();
            Post(payments);
        }

        public void Post(IEnumerable<AccountPayment> self)
        {
            var seqObj = DependencyResolver.Current.GetService<IRSequenceService>();
            var moveObj = DependencyResolver.Current.GetService<AccountMoveService>();
            var moves = new List<AccountMove>();
            foreach (var rec in self)
            {
                if (rec.State != "draft")
                    throw new Exception("Chỉ những thanh toán nháp mới được vào sổ.");
                string sequenceCode = "";
                if (rec.PaymentType == "transfer")
                {
                    sequenceCode = "account.payment.transfer";
                }
                else
                {
                    if (rec.PartnerType == "customer")
                    {
                        if (rec.PaymentType == "outbound")
                            sequenceCode = "account.payment.customer.refund";
                        if (rec.PaymentType == "inbound")
                            sequenceCode = "account.payment.customer.invoice";
                    }
                    if (rec.PartnerType == "supplier")
                    {
                        if (rec.PaymentType == "outbound")
                            sequenceCode = "account.payment.supplier.invoice";
                        if (rec.PaymentType == "inbound")
                            sequenceCode = "account.payment.supplier.refund";
                    }
                }

                rec.Name = seqObj.NextByCode(sequenceCode);
                var amount = rec.Amount * (rec.PaymentType == "outbound" || rec.PaymentType == "transfer" ? 1 : -1);
                var move = _CreatePaymentEntry(rec, amount);
                moves.Add(move);
                rec.State = "posted";
            }

            moveObj.Create(moves);
            moveObj.Post(moves);
            SaveChanges();
        }

        private AccountMove _CreatePaymentEntry(AccountPayment rec, decimal amount)
        {
            var amlObj = DependencyResolver.Current.GetService<AccountMoveLineService>();
            var moveObj = DependencyResolver.Current.GetService<AccountMoveService>();

            var debit = amount > 0 ? amount : 0;
            var credit = amount < 0 ? -amount : 0;

            var move = _GetMoveVals(rec);

            var counterpartAml = _GetSharedMoveLineVals(rec, debit, credit, move);
            var vals = _GetCounterpartMoveLineVals(rec);
            counterpartAml.Name = vals.Name;
            counterpartAml.Journal = vals.Journal;
            counterpartAml.JournalId = vals.JournalId;
            counterpartAml.Account = vals.Account;
            counterpartAml.AccountId = vals.AccountId;
            counterpartAml.PaymentId = vals.PaymentId;
            counterpartAml.Payment = vals.Payment;
            counterpartAml.CompanyId = vals.Account.CompanyId;
            counterpartAml.Company = vals.Account.Company;

            //Write counterpart lines
            var liquidityAmlDict = _GetSharedMoveLineVals(rec, credit, debit, move);
            var vals2 = _GetLiquidityMoveLineVals(rec, -amount);
            liquidityAmlDict.Name = vals2.Name;
            liquidityAmlDict.Journal = vals2.Journal;
            liquidityAmlDict.JournalId = vals2.JournalId;
            liquidityAmlDict.Account = vals2.Account;
            liquidityAmlDict.AccountId = vals2.AccountId;
            liquidityAmlDict.PaymentId = vals2.PaymentId;
            liquidityAmlDict.Payment = vals2.Payment;
            liquidityAmlDict.CompanyId = vals2.Account.CompanyId;
            liquidityAmlDict.Company = vals2.Account.Company;

            move.MoveLines = new List<AccountMoveLine>() { counterpartAml, liquidityAmlDict };
            return move;
        }

        private AccountMoveLine _GetLiquidityMoveLineVals(AccountPayment rec, decimal amount)
        {
            var name = rec.Name;
            if (rec.PaymentType == "transfer")
            {
                name = string.Format("Transfer to {0}", rec.Journal.Name);
            }

            var account = rec.PaymentType == "outbound" || rec.PaymentType == "transfer" ? rec.Journal.DefaultDebitAccount : rec.Journal.DefaultCreditAccount;
            var currency = rec.CurrencyId != rec.Company.CurrencyId ? rec.Currency : null;
            var vals = new AccountMoveLine
            {
                Name = name,
                AccountId = account.Id,
                Account = account,
                PaymentId = rec.Id,
                Payment = rec,
                JournalId = rec.JournalId,
                Journal = rec.Journal,
            };

            return vals;
        }

        private AccountMoveLine _GetCounterpartMoveLineVals(AccountPayment rec)
        {
            string name = "";
            if (rec.PaymentType == "transfer")
                name = rec.Name;
            else
            {
                name = "";
                if (rec.PartnerType == "customer")
                {
                    if (rec.PaymentType == "inbound")
                        name += "Khách hàng nạp tiền";
                    else if (rec.PaymentType == "outbound")
                        name += "Hoàn tiền rút tiền";
                }
                else if (rec.PartnerType == "supplier")
                {
                    if (rec.PaymentType == "inbound")
                        name += "Nhà cung cấp hoàn tiền";
                    else if (rec.PaymentType == "outbound")
                        name += "Thanh toán nhà cung cấp ";
                }
            }

            var account = _ComputeDestinationAccount(rec);
            return new AccountMoveLine
            {
                Name = name,
                Account = account,
                AccountId = account.Id,
                Company = account.Company,
                CompanyId = account.CompanyId,
                Journal = rec.Journal,
                JournalId = rec.JournalId,
                Payment = rec,
                PaymentId = rec.Id
            };
        }

        public Account _ComputeDestinationAccount(AccountPayment rec)
        {
            return rec.Company.AccountReceivable;
        }

        private AccountMove _GetMoveVals(AccountPayment rec, AccountJournal journal = null)
        {
            var seqObj = DependencyResolver.Current.GetService<IRSequenceService>();
            journal = journal ?? rec.Journal;
            if (journal.Sequence == null)
                throw new Exception("The journal " + journal.Name + " does not have a sequence, please specify one.");
            var name = seqObj.GetId(journal.SequenceId);
            return new AccountMove
            {
                Name = name,
                Date = rec.PaymentDate,
                Ref = rec.Communication,
                CompanyId = rec.CompanyId,
                Company = rec.Company,
                JournalId = journal.Id,
                Journal = journal,
            };
        }

        private AccountMoveLine _GetSharedMoveLineVals(AccountPayment rec, decimal? debit, decimal? credit, AccountMove move)
        {
            var partnerObj = DependencyResolver.Current.GetService<PartnerService>();
            var partner = rec.PaymentType == "inbound" || rec.PaymentType == "outbound" ? rec.Partner : null;
            return new AccountMoveLine
            {
                PartnerId = partner != null ? partner.Id : (long?)null,
                Move = move,
                MoveId = move.Id,
                Debit = debit,
                Credit = credit,
                Ref = move.Ref,
                Date = move.Date,
                Journal = move.Journal,
                JournalId = move.JournalId,
            };
        }
    }
}
