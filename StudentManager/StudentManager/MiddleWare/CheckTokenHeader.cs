using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace StudentManager.MiddleWare
{
    public static class RequestTokenHeader
    {
        public static IApplicationBuilder checkToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckTokenHeader>();
        }
    }
    public class CheckTokenHeader
    {
        private readonly RequestDelegate _next;
        public CheckTokenHeader(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            bool IsValid = false;
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var basicToken = context.Request.Headers["Authorization"].ToString();
                basicToken = basicToken.Replace("Basic ", "");
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage message = httpClient.GetAsync("https://studentmanager20181225060217.azurewebsites.net/api/MyCredentials/check-token?id=" + basicToken).Result;
                if (message.StatusCode == HttpStatusCode.OK)
                {
                    IsValid = true;
                }
            }
            if (IsValid)
            {
                context.Response.StatusCode = 200;
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Forbidden");
            }

        }

    }
}
