using DataLibrary;
using DataLibrary.Interfaces;
using DevBlog_BlazorServer.Interfaces;
using DevBlog_BlazorServer.Models;
using DevBlog_BlazorServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Dapper;
using AspNetCore.Identity.Dapper;
using Identity.Dapper;
using Identity.Dapper.Entities;
using System;
using Identity.Dapper.MySQL.Connections;
using Identity.Dapper.MySQL.Models;
using Identity.Dapper.Models;

namespace DevBlog_BlazorServer
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Identity / Dapper config
            services.ConfigureDapperConnectionProvider<MySqlConnectionProvider>(Configuration.GetSection("DapperIdentity"))
                .ConfigureDapperIdentityCryptography(Configuration.GetSection("DapperIdentityCryptography"))
                .ConfigureDapperIdentityOptions(new DapperIdentityOptions { UseTransactionalBehavior = false });

            services.AddIdentity<UserModel, Role>(x =>
            {
                x.Password.RequireDigit = false;
                x.Password.RequiredLength = 1;
                x.Password.RequireLowercase = false;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireUppercase = false;
            })
            .AddDapperIdentityFor<MySqlConfiguration>()
            .AddDefaultTokenProviders();

            //services.AddIdentity<DapperIdentityUser<Guid>, DapperIdentityRole<Guid>>()
            //        .AddDapperIdentityFor<MySqlConfiguration, Guid>();

            // authentication config
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole
                (Role.Admin));
            });

            services.AddTransient<CommentModel>();
            services.AddTransient<IPostService, PostService>();
            services.AddSingleton<IDataAccess, DataAccess>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}