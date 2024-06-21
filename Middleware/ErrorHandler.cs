using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using WebApi_Project_Internal.AuthorizationFilters.Services;

namespace BackEnd.Middleware
{

    public class ErrorHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorModel(500, context.Exception.Message, context.Exception.StackTrace?.ToString());
            var resultMessage = JsonConvert.SerializeObject(error);
            context.Result = new JsonResult(resultMessage);
        }
    }

    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ErrorMessage { get; set; }


        public ErrorModel(int statusCode, string message, string errorMessage)
        {
            StatusCode = statusCode;
            Message = message;
            ErrorMessage = errorMessage;
        }
    }
}