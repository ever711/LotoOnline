using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class IRModelAccessDTO
    {
        public IRModelAccessDTO()
        {
            Active = true;
            PermCreate = true;
            PermWrite = true;
            PermUnlink = true;
            PermRead = true;
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public bool? Active { get; set; }

        public bool? PermRead { get; set; }

        public bool? PermWrite { get; set; }

        public bool? PermCreate { get; set; }

        public bool? PermUnlink { get; set; }

        public long ModelId { get; set; }
        public IRModelDTO Model { get; set; }

        public long? GroupId { get; set; }
        public ResGroupDTO Group { get; set; }
    }
}
