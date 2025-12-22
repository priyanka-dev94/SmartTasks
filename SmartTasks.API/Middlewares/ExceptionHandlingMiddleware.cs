using Microsoft.AspNetCore.Mvc;
using SmartTasks.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace SmartTasks.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(
             HttpContext context,
             Exception exception)
        {
            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                ConflictException => HttpStatusCode.Conflict,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var problemDetails = new ProblemDetails
            {
                Status = (int)statusCode,
                Title = GetTitle(statusCode),
                Detail = exception.Message,
                Type = $"https://httpstatuses.com/{(int)statusCode}",
                Instance = context.Request.Path
            };

            problemDetails.Extensions["traceId"] = context.TraceIdentifier;

            if (_env.IsDevelopment())
            {
                problemDetails.Extensions["stackTrace"] = exception.StackTrace;
            }

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status.Value;

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(problemDetails));
        }

        private static string GetTitle(HttpStatusCode statusCode) =>
            statusCode switch
            {
                HttpStatusCode.NotFound => "Resource not found",
                HttpStatusCode.BadRequest => "Bad request",
                HttpStatusCode.Conflict => "Conflict",
                _ => "Internal server error"
            };
    }
}
