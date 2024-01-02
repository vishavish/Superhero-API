﻿using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Superhero.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await next(context);
			}
			catch (Exception e)
			{
                _logger.LogError(e, e.Message);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails problem = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = "Server Error",
                    Title = "Server Error",
                    Detail = "An internal server error has occured."
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));

                context.Response.ContentType = "application/json";
			}
        }
    }
}
