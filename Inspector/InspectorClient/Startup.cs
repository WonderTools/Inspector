﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WonderTools.Inspector;

namespace InspectorClient
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            BuildConfiguration(env);
        }

        public IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddInspector();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseInspector(x =>
            {
                x.AddName("Service Name - Sample Service");
                x.AddConfigurationSection(Configuration, "Node1:Node2:1");
                x.SetBaseEndpoint("/Hello/something");
            });


            //app.UseInspector(x =>
            //{
            //    x.AddConfigurationSection(Configuration, "ConfigurationData");
            //    x.AddName("Service Name - Sample Service");
            //    x.AddVersion("1.0.2.320");
            //    x.AddEnvironment("development");
            //    x.AddKeyValue("key", "Value");
            //    x.SetBaseEndpoint("/inspector");
            //    x.EnableCors();
            //    x.UseAuthenticationHeader("somevalue");
            //    x.AuthenticateWith(IsValid);
            //    x.AuthenticateWith("nachi", "seetha", "suchendra");
            //});

            //bool IsValid(string value)
            //{
            //    return true;
            //}


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        private void BuildConfiguration(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true);
            Configuration = builder.Build();
        }
    }
}
