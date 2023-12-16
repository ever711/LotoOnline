using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class ResGroupDTO
    {
        public ResGroupDTO()
        {
            Implieds = new List<ResGroupDTO>();
            Rules = new List<IRRuleDTO>();
            Users = new List<ApplicationUserDTO>();
            ModelAccesses = new List<IRModelAccessDTO>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<IRModelAccessDTO> ModelAccesses { get; set; }

        public IEnumerable<ResGroupDTO> Implieds { get; set; }

        public IEnumerable<IRRuleDTO> Rules { get; set; }

        public IEnumerable<ApplicationUserDTO> Users { get; set; }
    }
}
