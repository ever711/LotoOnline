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
    public class AccountService : Service<Account>
    {
        public AccountService(IRepository<Account> repository)
            : base(repository)
        {
        }

        public Account Create(Account vals)
        {
            vals.InternalType = vals.UserType.Type;
            Insert(vals);
            return vals;
        }
    }
}
