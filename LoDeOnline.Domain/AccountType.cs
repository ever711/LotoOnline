using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// Loại tài khoản
    /// </summary>
    public class AccountType: Entity
    {
        public AccountType()
        {
            IncludeInitialBalance = false;
        }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Note { get; set; }

        public bool? IncludeInitialBalance { get; set; }
    }
}
