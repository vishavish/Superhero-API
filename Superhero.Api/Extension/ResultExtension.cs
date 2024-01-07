using Microsoft.AspNetCore.Mvc;
using Superhero.Api.Models;

namespace Superhero.Api.Extension
{
    public static class ResultExtension
    {
        public static ProblemDetails ToProblem<T>(this Result<T> result, int statusCode)
        {
            return new()
            {
                Status = statusCode,
                Title = statusCode switch
                        {
                            404 => "Resource Not Found",
                            403 => "Forbidden Resource",
                            401 => "Unauthorized Access",
                            _ => "Internal Server Error"
                        },
                Detail = result.Message
            };
        }
    }
}
