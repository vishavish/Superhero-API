using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Net;

namespace Superhero.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                //var errorsInModelState = context.ModelState
                //       .Where(x => x.Value.Errors.Count > 0)
                //       .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage)).ToArray();


                //ProblemDetails problem = new()
                //{
                //    Status = (int)HttpStatusCode.BadRequest,
                //    Type = "Bad Request",
                //    Title = "Bad Request",
                //    Detail = "Bad request"
                //};

                //var json = JsonSerializer.Serialize(problem);

                //context.Result = new BadRequestObjectResult(json);
                //return;
            }

            await next();
        }
    }
}
