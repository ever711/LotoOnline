using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace LoDeOnline.Applications.Filters
{
    public class ODataExceptionCustomerFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var e = context.Exception;
            var message = e.Message;
            if (e.InnerException != null)
            {
                var innerException = e.InnerException;
                while (innerException != null)
                {
                    message = innerException.Message;
                    innerException = innerException.InnerException;
                }
            }

            var response = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            context.Response = response;
        }
    }
}