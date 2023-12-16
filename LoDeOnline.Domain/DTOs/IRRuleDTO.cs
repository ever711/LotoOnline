using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class IRRuleDTO
    {
        public IRRuleDTO()
        {
            Global = true;
            Active = true;
            PermCreate = true;
            PermWrite = true;
            PermUnlink = true;
            PermRead = true;
            Groups = new List<ResGroupDTO>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public bool? Global { get; set; }

        public bool? Active { get; set; }

        public bool? PermUnlink { get; set; }

        public bool? PermWrite { get; set; }

        public bool? PermRead { get; set; }

        public bool? PermCreate { get; set; }

        public long ModelId { get; set; }
        public IRModelDTO Model { get; set; }
        public string ModelName { get; set; }

        public string Code { get; set; }

        public string NameGet { get; set; }

        public IEnumerable<ResGroupDTO> Groups { get; set; }
    }
}
