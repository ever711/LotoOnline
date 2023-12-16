using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class PartnerDTO
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Ref { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool? Customer { get; set; }

        public long? CompanyId { get; set; }

        public bool? Active { get; set; }
    }
}
