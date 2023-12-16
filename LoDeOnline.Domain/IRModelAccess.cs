using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class IRModelAccess: Entity
    {
        public IRModelAccess()
        {
            Active = true;
            PermRead = true;
            PermCreate = true;
            PermWrite = true;
            PermUnlink = true;
        }

        public string Name { get; set; }

        public bool? Active { get; set; }

        public bool? PermRead { get; set; }

        public bool? PermWrite { get; set; }

        public bool? PermCreate { get; set; }

        public bool? PermUnlink { get; set; }

        public long ModelId { get; set; }

        public virtual IRModel Model { get; set; }

        public long? GroupId { get; set; }

        public virtual ResGroup Group { get; set; }
    }
}
