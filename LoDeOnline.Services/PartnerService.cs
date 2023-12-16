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
    public class PartnerService : Service<Partner>
    {
        public PartnerService(IRepository<Partner> repository)
            : base(repository)
        {
        }

        public Dictionary<long, PartnerCreditDebitItem> CreditDebitGet(IEnumerable<long> ids)
        {
            var amlObj = DependencyResolver.Current.GetService<AccountMoveLineService>();
            var types = new[] { "receivable", "payable" };
        
            var res = amlObj.Search(x => x.Move.State == "posted" && types.Contains(x.Account.InternalType) &&
            ids.Contains(x.Partner.Id) && x.Reconciled == false)
                .GroupBy(x => new { x.Partner, x.Account.InternalType })
                .Select(x => new
                {
                    Partner = x.Key.Partner,
                    Type = x.Key.InternalType,
                    Amount = x.Sum(s => s.AmountResidual)
                });

            var dict = ids.ToDictionary(x => x, x => new PartnerCreditDebitItem());
            foreach (var item in res)
            {
                var val = item.Amount ?? 0;

                if (item.Type == "receivable")
                    dict[item.Partner.Id].Credit = val;
                else if (item.Type == "payable")
                    dict[item.Partner.Id].Debit = -val;
            }

            return dict;
        }

        public IDictionary<long, PartnerCreditAvailable> CreditAvailable(IEnumerable<long> ids)
        {
            var amlObj = DependencyResolver.Current.GetService<AccountMoveLineService>();
            var paymentObj = DependencyResolver.Current.GetService<AccountPaymentService>();
            var aml_credit = amlObj.Search(x => x.Move.State == "posted" && x.Account.InternalType == "receivable" &&
            ids.Contains(x.Partner.Id) && x.Reconciled == false)
                .GroupBy(x => x.PartnerId)
                .Select(x => new
                {
                    PartnerId = x.Key,
                    Credit = x.Sum(s => s.AmountResidual)
                }).ToDictionary(x => x.PartnerId, x => x.Credit);

            var incomings = paymentObj.Search(x => ids.Contains(x.Partner.Id) && x.State == "draft" && x.PaymentType == "inbound"
            && x.PartnerType == "customer")
                .GroupBy(x => x.PartnerId)
                .Select(x => new
                 {
                     PartnerId = x.Key,
                    Credit = x.Sum(s => s.Amount),
                }).ToDictionary(x => x.PartnerId, x => x.Credit);

            var outgoings = paymentObj.Search(x => ids.Contains(x.Partner.Id) && x.State == "draft" && x.PaymentType == "outbound"
          && x.PartnerType == "customer")
              .GroupBy(x => x.PartnerId)
              .Select(x => new
              {
                  PartnerId = x.Key,
                  Credit = x.Sum(s => s.Amount),
              }).ToDictionary(x => x.PartnerId, x => x.Credit);

            var res = new Dictionary<long, PartnerCreditAvailable>();
            foreach (var id in ids)
            {
                var credit_available = aml_credit.ContainsKey(id) ? aml_credit[id] : 0;
                var incoming_credit = incomings.ContainsKey(id) ? incomings[id] : 0;
                var outgoing_credit = outgoings.ContainsKey(id) ? outgoings[id] : 0;
                var virtual_available = credit_available + incoming_credit - outgoing_credit;
                res.Add(id, new PartnerCreditAvailable {
                    CreditAvailable = credit_available ?? 0,
                    IncomingCredit = incoming_credit,
                    OutgoingCredit = outgoing_credit,
                    VirtualAvailable = virtual_available ?? 0
                });
            }

            return res;
        }

        public class PartnerCreditDebitItem
        {
            public decimal Credit { get; set; }

            public decimal Debit { get; set; }
        }

        public class PartnerCreditAvailable
        {
            public decimal VirtualAvailable { get; set; }

            public decimal CreditAvailable { get; set; }

            public decimal IncomingCredit { get; set; }

            public decimal OutgoingCredit { get; set; }
        }
    }
}
