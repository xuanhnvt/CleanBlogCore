using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace CleanBlog.Web.Pages.Blog
{
    public class BlogPostDetailsModel : PageModel
    {
        public readonly LinkGenerator _linkGenerator;

        public string RouteDataString = String.Empty;

        public BlogPostDetailsModel(LinkGenerator linkGenerator)
        {
            _linkGenerator = linkGenerator;
        }

        public void OnGet()
        {
            //JsonConvert.ToString()
            //Jso
            //foreach (var item in this.HttpContext.GetRouteData())
            var list = this.HttpContext.GetRouteData().Values.Select(item =>
            {
                return String.Format("{0}: {1}\n", item.Key, item.Value);
            }).ToList();
            list.ForEach(item => this.RouteDataString += item);

            //_linkGenerator.GetPathByName("GenericEndpoint", new { GenericSlug = "test-test"});
            //this.RouteData = JsonConvert.ToString(this.HttpContext.GetRouteData());
        }
    }
}