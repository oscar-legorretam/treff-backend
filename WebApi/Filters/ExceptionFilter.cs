using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace WebApi.API.Filters
{
    /// <summary>
    /// Manages the exceptions in controllers
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Create an ExceptionContext if an Exceptions is throwed
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            int statusCode = (int)HttpStatusCode.InternalServerError;

            if (exception is ArgumentException e)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            context.Result = new JsonResult(exception.Message)
            {
                StatusCode = statusCode
            };
        }
    }
}
