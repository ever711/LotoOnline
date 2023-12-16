using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Controllers
{
    public class BaseController : Controller
    {
        protected string GetModelStateError(ModelStateDictionary modelState)
        {
            return string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
        }
    }
}