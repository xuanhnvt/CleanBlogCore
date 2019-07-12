using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanBlog.Services.Security;
using CleanBlog.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace CleanBlog.Web.Pages
{
    //[Authorize(Policy = "ViewProjects")]
    [Authorize(Roles = "Admin")]
    [Permission(PermissionTypes.Common.AccessAdminPanel)]
    public class AboutModel : PageModel
    {
        IAuthorizationService test;

        public string UserClaims = String.Empty;

        public void OnGet()
        {
            //test.AuthorizeAsync()
          var list =  User.Claims.Select(c =>
    new
    {
        Type = c.Type,
        Value = c.Value
    }).ToList();
            //UserClaims = JsonConvert.SerializeObject(HttpContext.User.Claims);
            UserClaims = JsonConvert.SerializeObject(list);
        }
    }
}