using LoDeOnline.Applications.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.OData;

namespace LoDeOnline.Apis
{
    [ODataExceptionCustomerFilterAttribute]
    public class BaseController : ODataController
    {
    }
}
