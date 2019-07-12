using CleanBlog.Services.Security;
using CleanBlog.Web.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Nhx.Core.Entities.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanBlog.Web.Filters
{
    public class PermissionAttribute : TypeFilterAttribute
    {
        public PermissionAttribute(string permission) : base(typeof(PermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }

    public class PermissionFilter : IAsyncAuthorizationFilter
    {
        private readonly string _permissionRecord;
        private readonly IAuthorizationService _authorizationService;

        public PermissionFilter(string permission, IAuthorizationService authorizationService)
        {
            _permissionRecord = permission;
            _authorizationService = authorizationService;
        }
        
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var result = await _authorizationService.AuthorizeAsync(context.HttpContext.User, null, new PermissionRequirement(_permissionRecord));
            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
