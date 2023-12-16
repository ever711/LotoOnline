using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class DanhDeLineXien: Entity
    {
        public string SoXien { get; set; }

        public long? LineId { get; set; }
        public virtual DanhDeLine Line { get; set; }
    }
}
