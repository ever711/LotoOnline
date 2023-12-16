using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class CompanyService : Service<Company>
    {
        public CompanyService(IRepository<Company> repository)
            : base(repository)
        {
        }

        public Company GetCurrentCompany()
        {
            return Search().FirstOrDefault();
        }
    }
}
