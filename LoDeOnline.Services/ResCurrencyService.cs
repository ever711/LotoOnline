using LoDeOnline.Data;
using LoDeOnline.Domain;
using MyERP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class ResCurrencyService : Service<ResCurrency>
    {
        public ResCurrencyService(IRepository<ResCurrency> repository)
            : base(repository)
        {
        }

        public decimal Round(ResCurrency toCurrency, decimal fromAmount)
        {
            return (decimal)FloatUtils.FloatRound((double)(fromAmount), precisionRounding: (double?)toCurrency.Rounding);
        }

        public bool IsZero(ResCurrency currency, decimal amount)
        {
            return FloatUtils.FloatIsZero((double)amount, precisionRounding: (double?)currency.Rounding);
        }
    }
}
