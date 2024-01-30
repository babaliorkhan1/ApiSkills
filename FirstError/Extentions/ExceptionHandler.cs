using FirstError.Service.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace FirstError.Api.Extentions
{
    public static class ExceptionHandler
    {
        public static void CustomExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    string message = "İnternal error";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    int statuscode = (int)HttpStatusCode.InternalServerError;
                    context.Response.Redirect("home/index");
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    Exception exception = contextFeature.Error;
                    
                   
                    if (exception is ItemNotFoundException)
                    {
                        statuscode = 404;
                        message = exception.Message;

                    }

                    if (exception is ItemAlreadyExistException)
                    {
                        statuscode = 303;
                        message = exception.Message;
                    }
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = statuscode;
                        context.Response.Redirect("home/index");
                        context.Response.ContentType = "application/json";
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { statuscode = statuscode, message = message }));
                    }
                });
            });
        }
    }
}
