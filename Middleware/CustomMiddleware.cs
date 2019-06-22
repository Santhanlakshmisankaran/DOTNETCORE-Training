using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace trainingmiddleware.Middleware
{
    public class CustomMiddlewareOptions
    {
        public bool EnableLog { get; set; }
    }
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CustomMiddlewareOptions _options;
        private readonly IConfiguration _configuration;

        public CustomMiddleware(RequestDelegate next,
            IConfiguration configuration,
            CustomMiddlewareOptions options)
        {
            _next = next;
            _options = options;
            _configuration = configuration;
        }

        public Task Invoke(HttpContext httpContext)
        {
            bool isEnable = _configuration.GetValue<bool>("Log");
           if(_options.EnableLog == true)
            {
                Debug.WriteLine("Request : {0}", httpContext.Request);
                Debug.WriteLine("Request : {0}", httpContext.Response);
            }          
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder,CustomMiddlewareOptions options)
        {
            return builder.UseMiddleware<CustomMiddleware>(options);
        }
    }
}
