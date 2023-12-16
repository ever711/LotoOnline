using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class IRSequenceDTO
    {
        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int NumberNext { get; set; }

        public string Implementation { get; set; }

        public int Padding { get; set; }

        public int NumberIncrement { get; set; }

        public string Prefix { get; set; }

        public bool Active { get; set; }

        public string Suffix { get; set; }
    }
}
