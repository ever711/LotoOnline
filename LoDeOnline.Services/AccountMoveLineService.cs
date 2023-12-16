using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using MyERP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class AccountMoveLineService : Service<AccountMoveLine>
    {
        public AccountMoveLineService(IRepository<AccountMoveLine> repository)
            : base(repository)
        {
        }

        public void _Compute(IEnumerable<AccountMoveLine> self)
        {
            foreach (var line in self)
            {
                line.CompanyId = line.Account.CompanyId;
                line.Company = line.Account.Company;
                line.JournalId = line.Move.JournalId;
                line.Ref = line.Move.Ref;
                line.Date = line.Move.Date;
            }

            _StoreBalance(self);
            _AmountResidual(self);
        }

        private void _AmountResidual(IEnumerable<AccountMoveLine> self)
        {
            foreach (var line in self)
            {
                if (line.Account.Reconcile == false)
                {
                    line.Reconciled = false;
                    line.AmountResidual = 0;
                    continue;
                }

                var amount = Math.Abs((line.Debit ?? 0) - (line.Credit ?? 0));
                var sign = line.Debit - line.Credit > 0 ? 1 : -1;

                foreach (var partialLine in line.MatchedDebits.Concat(line.MatchedCredits))
                {
                    var signPartialLine = partialLine.CreditMove.Id == line.Id ? sign : (-1 * sign);
                    amount += signPartialLine * (partialLine.Amount ?? 0);
                }

                bool reconciled = false;
                var digitsRoundingPrecision = line.Company.Currency.Rounding;
                if (FloatUtils.FloatIsZero((double)amount, precisionRounding: (double?)digitsRoundingPrecision))
                {
                    reconciled = true;
                }

                line.Reconciled = reconciled;
                line.AmountResidual = line.Company.Currency.Round(amount * sign);
            }
        }

        private void _StoreBalance(IEnumerable<AccountMoveLine> self)
        {
            foreach (var line in self)
            {
                line.Balance = line.Debit - line.Credit;
            }
        }

        public ComputeAmountFieldsRes ComputeAmountFields(decimal amount, ResCurrency srcCurrency, ResCurrency companyCurrency, ResCurrency invoiceCurrency = null,
         DateTime? date = null)
        {
            decimal? amountCurrency = 0;
            ResCurrency currency = null;
            var debit = amount > 0 ? amount : 0;
            var credit = amount < 0 ? -amount : 0;
            return new ComputeAmountFieldsRes { Debit = debit, Credit = credit, AmountCurrency = amountCurrency, Currency = currency };
        }

        public class ComputeAmountFieldsRes
        {
            public decimal? Debit { get; set; }
            public decimal? Credit { get; set; }
            public decimal? AmountCurrency { get; set; }
            public ResCurrency Currency { get; set; }
        }

        public void Create(IEnumerable<AccountMoveLine> self)
        {
            _Compute(self);
            _CheckContraints(self);
            Insert(self);
        }

        public void _CheckContraints(IEnumerable<AccountMoveLine> lines)
        {
            foreach (var line in lines)
            {
                if (!(line.Credit * line.Debit == 0))
                    throw new Exception("Wrong credit or debit value in accounting entry !");
                if (!(line.Credit + line.Debit >= 0))
                    throw new Exception("Wrong credit or debit value in accounting entry !");
            }
        }
    }
}
