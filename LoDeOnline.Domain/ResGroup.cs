using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class ResGroup: Entity
    {
        public ResGroup()
        {
            Implieds = new List<ResGroup>();
            ModelAccesses = new List<IRModelAccess>();
            Users = new List<ApplicationUser>();
            Rules = new List<IRRule>();
        }

        public string Name { get; set; }

        public virtual IList<ApplicationUser> Users { get; set; }

        public virtual IList<IRModelAccess> ModelAccesses { get; set; }

        public virtual IList<IRRule> Rules { get; set; }

        public long? CategoryId { get; set; }
        public virtual IRModuleCategory Category { get; set; }

        public virtual IList<ResGroup> Implieds { get; set; }
    }
}
