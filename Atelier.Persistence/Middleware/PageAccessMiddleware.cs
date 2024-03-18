using Atelier.Application.Interfaces.Contexts;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Atelier.Persistence.Middleware
{
    public class PageAccessMiddleware
    {
        private readonly RequestDelegate _next;

        public PageAccessMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IDatabaseContext dbContext)
        {
            var user = context.User;
            var controllerName = context.Request.RouteValues["controller"];
            var actionName = context.Request.RouteValues["action"];
            var requestedUrl = $"{controllerName}/{actionName}";

            if (user.Identity.IsAuthenticated)
            {
                var hasAccess = dbContext.PageAccess.Any(pa =>
                    pa.UserId == user.FindFirst(ClaimTypes.NameIdentifier).Value &&
                    dbContext.Pages.Any(p => p.Url == requestedUrl && p.Id == pa.PageId && pa.CanAccess));

                if (!hasAccess)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
}
