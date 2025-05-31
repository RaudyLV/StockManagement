using System;
using System.Collections.Generic;

using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Exceptions;
using FluentValidation;

namespace Presentation.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try 
            {
                await _next(context);
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            switch(ex)
            {
                case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                var errors = validationException.Errors.Select(e => new {e.PropertyName, e.ErrorMessage});
                result = JsonSerializer.Serialize(errors);
                break;

                case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if(result == string.Empty)
            {
                result = JsonSerializer.Serialize(
                    new 
                    {
                        error = ex.Message,
                        ex.InnerException?.Message
                    }
                );
            }

            await context.Response.WriteAsync(result);
        }

    }
}