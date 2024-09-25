using KitaraKauppa.Service.AuthenticationService;
using KitaraKauppa.Service.CustomExceptions;
using KitaraKauppa.Service.Shared;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KitaraKauppa.Presentation.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            Log.Error(exception, "Unexpected Error");

            var response = new KKResult<string>()
                .Fail("Something critical happen. Please try again later or contact system administrator");

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            await context.Response.WriteAsync(payload);
        }
    }
}
