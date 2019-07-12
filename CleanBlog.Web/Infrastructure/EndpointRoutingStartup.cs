using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nhx.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using CleanBlog.Web.Seo;
using CleanBlog.Web.Routing;
using System.Threading.Tasks;
using CleanBlog.Web.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Identity;

namespace CleanBlog.Web.Framework.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring MVC on application startup
    /// </summary>
    public class EndpointRoutingStartup : INopStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages(options =>
            {
                /*options.Conventions.Add(
                new PageRouteTransformerConvention(
                new SlugifyParameterTransformer()));*/
                /*options.Conventions.AddFolderRouteModelConvention("/", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        selector.AttributeRouteModel = new AttributeRouteModel
                        {
                            Order = -1,
                            Template = AttributeRouteModel.CombineTemplates(
                                "{lang?}",
                                selector.AttributeRouteModel.Template),
                        };
                    }
                });*/
            }).AddNewtonsoftJson();

            //UserClaimsPrincipalFactory<>
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            //application.UseMiddleware<CustomEndpointMiddleware>();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.MapFallbackToGenericPage();
            });
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            //MVC should be loaded last
            get { return 1000; }
        }
    }
}