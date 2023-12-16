using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public static class ResCurrencyExtensions
    {
        public static decimal Round(this ResCurrency currency, decimal amount)
        {
            return DependencyResolver.Current.GetService<ResCurrencyService>().Round(currency, amount);
        }

        public static bool IsZero(this ResCurrency currency, decimal amount)
        {
            return DependencyResolver.Current.GetService<ResCurrencyService>().IsZero(currency, amount);
        }
    }
}
