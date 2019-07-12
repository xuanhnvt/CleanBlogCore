using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace CleanBlog.Web.ApplicationModels
{
    public class GlobalTemplatePageRouteModelConvention : IPageRouteModelConvention
    {
        public void Apply(PageRouteModel model)
        {
            var isIndexPage = model.ViewEnginePath.EndsWith("/Index", StringComparison.OrdinalIgnoreCase);

            foreach (var selector in model.Selectors.ToList())
            {
                var template = selector.AttributeRouteModel.Template;
                if (isIndexPage)
                {
                    var isIndexRoute = template.EndsWith("Index", StringComparison.OrdinalIgnoreCase);
                    if (isIndexRoute)
                    {
                        model.Selectors.Remove(selector);
                        continue;
                    }
                }
                selector.AttributeRouteModel.Template =
                    AttributeRouteModel.CombineTemplates(template,
                        "{handler?}/{id?}");
            }
        }
    }
}
