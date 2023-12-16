using LoDeOnline.Data;
using LoDeOnline.Domain;
using LoDeOnline.Domain.DTOs;
using Microsoft.AspNet.Identity;
using MyERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class IRModelDataService : Service<IRModelData>
    {
        private const string CACHE_KEY = "ir.model.data.{0}";
        public IRModelDataService(IRepository<IRModelData> repository)
            : base(repository)
        {
        }

        public object GetRef(string reference)
        {
            var tmp = reference.Split('.');
            var module = tmp[0];
            var name = tmp[1];
            var data = Table.Where(x => x.Module == module && x.Name == name).FirstOrDefault();
            if (data == null)
                return null;
            switch (data.Model)
            {
                case "res.groups":
                    {
                        return DependencyResolver.Current.GetService<ResGroupService>()
                            .GetById(data.ResId.Value);
                    }
                case "res.users":
                    {
                        return DependencyResolver.Current.GetService<ApplicationUserManager>()
                            .FindById(data.Res2Id);
                    }
                case "account.account.type":
                    {
                        return DependencyResolver.Current.GetService<AccountTypeService>()
                            .GetById(data.ResId);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
