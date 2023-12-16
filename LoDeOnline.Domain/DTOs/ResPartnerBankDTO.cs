using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class ResPartnerBankDTO
    {
        public long Id { get; set; }

        public string AccNumber { get; set; }

        public long? CompanyId { get; set; }
    }
}
