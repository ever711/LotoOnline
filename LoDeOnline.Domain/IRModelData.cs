using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class IRModelData: Entity
    {
        public string Name { get; set; }

        public long? ResId { get; set; }

        public string Model { get; set; }

        public string Module { get; set; }

        public string Res2Id { get; set; }
    }
}
