using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nhx.Core.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace CleanBlog.Web.Framework.Infrastructure
{
    /// <summary>
    /// Represents object for the configuring common features and middleware on application startup
    /// </summary>
    public class CommonStartup : INopStartup
    {

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //compression
            //services.AddResponseCompression();

            //add options feature
            //services.AddOptions();

            //add memory cache
            //services.AddMemoryCache();

            //add distributed memory cache
            //services.AddDistributedMemoryCache();

            //add HTTP sesion state feature
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddCors();
            /*services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://example.com",
                                        "http://localhost:4200");
                });
            });*/

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()));


            //add anti-forgery
            //services.AddAntiforgery();

            //add localization
            //services.AddLocalization();
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseCookiePolicy();
            //application.UseCors(MyAllowSpecificOrigins);

            application.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
            application.UseCors("AllowAll");
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order
        {
            //common services should be loaded after error handlers
            get { return 100; }
        }
    }
}