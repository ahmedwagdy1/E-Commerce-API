using Microsoft.AspNetCore.Http ;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModel;

namespace E_Commerce.API.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustmValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                .Where(error => error.Value?.Errors.Any() == true).Select(error => new ValidationErrors()
                {
                    Field = error.Key,
                    Errors = error.Value?.Errors.Select(error => error.ErrorMessage) ?? new List<string>()
                });
            var response = new ValidationErrorResponse()
            {
                ErrorMessage = "one or more validation error happened",
                StatusCode = StatusCodes.Status400BadRequest,
                Errors = errors
            };
            return new BadRequestObjectResult(response);
        }
    }
}
