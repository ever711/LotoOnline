using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class AccountPartialReconcile: Entity
    {
        public long DebitMoveId { get; set; }
        public virtual AccountMoveLine DebitMove { get; set; }

        public long CreditMoveId { get; set; }
        public virtual AccountMoveLine CreditMove { get; set; }

        public decimal? Amount { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public long? FullReconcileId { get; set; }
        public virtual AccountFullReconcile FullReconcile { get; set; }
    }
}
