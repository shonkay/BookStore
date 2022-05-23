using BookStore.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookStore.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var result = httpContext.Response;
                var response = new ResponseModel();

                response.ResponseCode = HttpStatusCode.InternalServerError;
                response.ResponseData = $"Error Message : {ex.Message}. {ex.StackTrace}";
                response.ResponseMessage = "An Error Occured in the Application";

                var stringResult = JsonConvert.SerializeObject(response);

                Log.Error(stringResult);
                result.HttpContext.Response.StatusCode = (int)response.ResponseCode;
                await result.WriteAsync(stringResult);
            }
          
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
