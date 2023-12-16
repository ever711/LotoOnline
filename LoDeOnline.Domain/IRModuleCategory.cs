using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class IRModuleCategory: Entity
    {
        public IRModuleCategory()
        {
            Visible = true;
        }

        public string Name { get; set; }

        public int? Sequence { get; set; }

        public long? ParentId { get; set; }
        public virtual IRModuleCategory Parent { get; set; }

        public bool? Visible { get; set; }

        public string Description { get; set; }
    }
}
