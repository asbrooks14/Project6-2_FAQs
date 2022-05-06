﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FAQs.Models;

namespace FAQs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = true;
            });

            services.AddControllersWithViews();

            services.AddDbContext<FAQContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("FAQContext")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting(); // mark where routing decisions are made
                              // configure middleware that runs after routing decisions have been made
            app.UseEndpoints(endpoints => // map the endpoints
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "topic_and_category",
                   pattern: "{controller}/{action}/topic/{topicId}/category/{catId}");
                endpoints.MapControllerRoute(
                    name: "topic",
                    pattern: "{controller}/{action}/topic/{topicId}");
                endpoints.MapControllerRoute(
                    name: "category",
                    pattern: "{controller}/{action}/category/{catId}");
            });
            // configure other middleware here
        }
    }
}
