using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Domain.DTOs
{
    public class ApplicationUserDTO
    {
        public ApplicationUserDTO()
        {
            Id = "";
            Active = true;
            Groups = new List<ResGroupDTO>();
        }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public long CompanyId { get; set; }

        public bool? Active { get; set; }

        public IEnumerable<ResGroupDTO> Groups { get; set; }
    }
}
