using LoDeOnline.Data;
using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LoDeOnline.Services
{
    public class IRModelService : Service<IRModel>
    {
        public IRModelService(IRepository<IRModel> modelRepository)
            : base(modelRepository)
        {
        }
    }
}
