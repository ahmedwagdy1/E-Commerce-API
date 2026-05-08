using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModel;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
    public class ApiController : ControllerBase
    {
    }
}
