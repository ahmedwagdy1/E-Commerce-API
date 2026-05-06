using Azure;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModel;

namespace E_Commerce.API.Middlewares
{
    public class GlobalExceptionsHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionsHandlingMiddleware> _logger;

        public GlobalExceptionsHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionsHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // not found error , internal service error throw ex
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"something went wrong => : {ex.Message}");
                await HandleExceptionsAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatuseCode = StatusCodes.Status404NotFound,
                Message = $"the endpoint with url {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            // 1] change StatusCode
            //context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.StatusCode = ex switch
            {
                NotFoundExceptions => StatusCodes.Status404NotFound,
                (_) => StatusCodes.Status500InternalServerError
            };

            // 2] change content type
            context.Response.ContentType = "application/json";

            // 3] write Response in body
            var response = new ErrorDetails()
            {
                StatuseCode = context.Response.StatusCode,
                Message = ex.Message,
            }.ToString();
            await context.Response.WriteAsync(response);
        }
    }
}
