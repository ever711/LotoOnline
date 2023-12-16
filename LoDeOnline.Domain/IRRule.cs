using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class IRRule: Entity
    {
        public IRRule()
        {
            Global = true;
            Active = true;
            PermCreate = true;
            PermWrite = true;
            PermUnlink = true;
            PermRead = true;
            Groups = new List<ResGroup>();
        }
        public string Name { get; set; }

        public bool? Global { get; set; }

        public bool? Active { get; set; }

        public bool? PermUnlink { get; set; }

        public bool? PermWrite { get; set; }

        public bool? PermRead { get; set; }

        public bool? PermCreate { get; set; }

        public long ModelId { get; set; }
        public virtual IRModel Model { get; set; }

        public virtual IList<ResGroup> Groups { get; set; }

        public string DomainForce { get; set; }

        public string Code { get; set; }
    }
}
