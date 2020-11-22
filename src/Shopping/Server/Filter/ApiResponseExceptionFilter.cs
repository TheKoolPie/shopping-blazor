using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Shopping.Server.Filter
{
    public class ApiResponseExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _hostEnv;

        public ApiResponseExceptionFilter(IWebHostEnvironment hostEnv)
        {
            _hostEnv = hostEnv;
        }
        public void OnException(ExceptionContext context)
        {
            if (_hostEnv.IsDevelopment())
            {
                return;
            }

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
