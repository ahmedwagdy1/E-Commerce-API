using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using System.Text;

namespace Persistance.Attribute
{
    internal class RedisCashAttribute(int duration = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashService = context.HttpContext.RequestServices.GetRequiredService<IServiceManger>().CashService;
            string key = GenerateKey(context.HttpContext.Request);
            var result = await cashService.GetAsync(key);
            if(result != null)
            {
                context.Result = new ContentResult()
                {
                    Content = result,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            var resultContext = await next.Invoke();
            if (resultContext.Result is OkObjectResult okObject)
            {
                await cashService.SetAsync(key, okObject, TimeSpan.FromSeconds(duration));
            }
        }

        private string GenerateKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path); // /api/Product
            foreach (var item in request.Query.OrderBy(o => o.Key))
            {
                key.Append($"-{item.Key}-{item.Value}");
            }
            return key.ToString();
        }
    }
}
