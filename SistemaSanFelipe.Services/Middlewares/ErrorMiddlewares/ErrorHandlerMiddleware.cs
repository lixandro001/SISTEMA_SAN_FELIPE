using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SistemaSanFelipe.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SistemaSanFelipe.Services.Middlewares.ErrorMiddlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ErrorHandlerAsync(context, ex);
            }
        }

        private async Task ErrorHandlerAsync(HttpContext context, Exception ex)
        {
            string message = null;

            context.Response.ContentType = "application/json";

            switch (ex)
            {
                case ErrorHandler eh:

                    context.Response.StatusCode = (int)eh.Code;
                    message = eh.Message;

                    break;

                case Exception e:

                    message = string.IsNullOrWhiteSpace(e.Message) ? "Error desconocido" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    break;
            }

            if (message != null)
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(MessageResult.Of(message)));
            }
        }

    }
}
