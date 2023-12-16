using LoDeOnline.Services;
using MyERP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace LoDeOnline.Applications.Attributes
{
    public class AdminAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var resGroupObj = DependencyResolver.Current.GetService<ResGroupService>();

            if (!resGroupObj.HasGroup("base.group_user"))
            {
                HandleUnauthorizedRequest(filterContext);
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}