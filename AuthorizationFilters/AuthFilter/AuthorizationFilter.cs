using System.Net;
using System.Threading.Tasks;
using BackEnd.AuthorizationFilters.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BackEnd.AuthorizationFilters.AuthFilter
{


    //public class AuthorizationFilter
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly IConfiguration _configuration;

    //    public AuthorizationFilter(RequestDelegate next, IConfiguration configuration)
    //    {
    //        _next = next;
    //        _configuration = configuration;
    //    }

    //    public async Task InvokeAsync(HttpContext context)
    //    {
    //        var currentHttpRequestKey = context.Request.Headers.TryGetValue(AuthContext.AuthHeader, out var extractedApiKey);

    //        if (!currentHttpRequestKey)
    //        {
    //            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    //            await context.Response.WriteAsync("API key is required");
    //            return;
    //        }

    //        var apiKey = _configuration.GetValue<string>(AuthContext.AuthString);
    //        if (apiKey == null)
    //        {
    //            new UnauthorizedResult();
    //        }

    //        if (!apiKey.Equals(extractedApiKey.ToString()))
    //        {
    //            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    //            await context.Response.WriteAsync("Invalid API Key");
    //            return;
    //        }

    //        await _next(context);
    //    }
    //}
      
          public class ApiKeyValidation : IAuthorizationFilter
        {
            private readonly IConfiguration _configuration;

            public ApiKeyValidation(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var errors = new Dictionary<string, List<string>>();
                var headerApiExists = context.HttpContext.Request.Headers.TryGetValue(AuthContext.AuthHeader, out var filteredApiKey);

                if (!headerApiExists || string.IsNullOrWhiteSpace(filteredApiKey))
                {
                    errors["API-x-IKM"] = new List<string>() { "API key is required." };
                }
                else
                {
                    var expectedApiKey = _configuration.GetValue<string>(AuthContext.AuthString);

                    if (string.IsNullOrEmpty(expectedApiKey))
                    {
                        errors["API-x-IKM"] = new List<string>() { "Invalid API key configuration." };
                    }
                    else if (!filteredApiKey.Equals(expectedApiKey))
                    {
                        errors["API-x-IKM"] = new List<string>() { "Invalid API key." };
                    }
                }

                if (errors.Count > 0)
                {
                    context.Result = new BadRequestObjectResult(new
                    {
                        error = new
                        {
                            message = "A validation error occurred.",
                            code = "validation_error",
                            errors
                        },
                        details = new { className = GetType().Name }
                    });
                }
            }
        }

    }

