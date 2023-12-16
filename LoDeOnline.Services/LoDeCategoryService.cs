using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using MyERP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class LoDeCategoryService : Service<LoDeCategory>
    {
        public LoDeCategoryService(IRepository<LoDeCategory> repository)
            : base(repository)
        {
        }

        public void Create(LoDeCategory self)
        {
            self.Slug = StringUtils.CreateUrl(self.Name, "-");
            Insert(self);
        }

        public void Write(LoDeCategory self)
        {
            self.Slug = StringUtils.CreateUrl(self.Name, "-");
            Update(self);
        }
    }
}
