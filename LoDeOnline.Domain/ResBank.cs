using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain
{
    public class ResBank: Entity
    {
        public string Name { get; set; }

        public bool? Active { get; set; }

        /// <summary>
        /// Bank Identifier Code
        /// </summary>
        public string BIC { get; set; }
    }
}
