using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    /// <summary>
    /// nhà cung cấp/ khách hàng
    /// </summary>
    public class Partner : Entity
    {
        public Partner()
        {
            Customer = true;
            Active = true;
        }

        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Ref { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public bool? Customer { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public bool? Active { get; set; }
    }
}
