using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class IRSequence: Entity
    {
        public IRSequence()
        {
            Active = true;
            NumberIncrement = 1;
            NumberNext = 1;
        }

        public string Code { get; set; }

        public string Name { get; set; }

        public int NumberNext { get; set; }

        public string Implementation { get; set; }

        public int Padding { get; set; }

        public int NumberIncrement { get; set; }

        public string Prefix { get; set; }

        public bool Active { get; set; }

        public string Suffix { get; set; }

        public long? CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
