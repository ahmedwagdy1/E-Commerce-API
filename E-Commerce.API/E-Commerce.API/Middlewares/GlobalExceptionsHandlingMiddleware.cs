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

            context.Response.ContentType = "application/json";

            var response = new ErrorDetails()
            {
                Message = ex.Message,
            };

            context.Response.StatusCode = ex switch
            {
                NotFoundExceptions => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                ValidationsExceptions validationsExceptions => HandleValidationException(validationsExceptions, response),
                (_) => StatusCodes.Status500InternalServerError
            };
            
            response.StatuseCode = context.Response.StatusCode;
            await context.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(ValidationsExceptions validationsExceptions, ErrorDetails response)
        {
            response.Errors = validationsExceptions.Errors;
            return StatusCodes.Status400BadRequest;
        }
    }
}
