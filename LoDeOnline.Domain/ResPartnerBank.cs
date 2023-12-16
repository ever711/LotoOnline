using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class ResPartnerBank : Entity
    {
        public string AccNumber { get; set; }

        public string SanitizedAccNumber { get; set; }

        public long? PartnerId { get; set; }
        public virtual Partner Partner { get; set; }

        public long? BankId { get; set; }
        public virtual ResBank Bank { get; set; }

        public int? Sequence { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
