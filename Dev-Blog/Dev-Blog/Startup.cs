using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dev_Blog.Data;
using Dev_Blog.Models;
using Dev_Blog.Models.Base;
using Dev_Blog.Models.Interfaces;
using Dev_Blog.Models.Services;
using Dev_Blog.Models.ViewModels;
using ECommerce.Models.Interfaces;
using ECommerce.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySqlConnector;
using Dropbox.Api;

namespace Dev_Blog
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DevBlogDB"));
            });

            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("DevBlogUserDB"));
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole(Role.Admin));
            });

            services.AddScoped<IImage, ImageService>();
            services.AddTransient<IPost, PostService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleInitializer.SeedData(serviceProvider, userManager, Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}