using LoDeOnline.Applications.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoDeOnline.Areas.Admin.Controllers
{
    [Authorize]
    [AdminAuthorizeAttribute]
    public class BaseController : Controller
    {
    }
}