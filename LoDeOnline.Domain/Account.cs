using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Tài khoản kế toán
    /// </summary>
    public class Account: Entity
    {
        public Account()
        {
            Active = true;
            Reconcile = false;
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public long UserTypeId { get; set; }
        public virtual AccountType UserType { get; set; }

        public bool? Active { get; set; }

        public string Note { get; set; }

        public long CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long? CurrencyId { get; set; }
        public ResCurrency Currency { get; set; }

        //UserType.Type
        public string InternalType { get; set; }

        public bool? Reconcile { get; set; }
    }
}
