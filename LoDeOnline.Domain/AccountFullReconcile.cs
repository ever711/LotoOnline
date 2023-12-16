using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class AccountFullReconcile : Entity
    {
        public string Name { get; set; }

        public virtual IList<AccountPartialReconcile> PartialReconciles { get; set; }

        public virtual IList<AccountMoveLine> ReconciledLines { get; set; }
    }
}
