using BlazorServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorServer.Models;
using BlazorServer.Interfaces;
using BlazorServer.Services;
using Blazored.Modal;
using BlazorServer.State;
using System;

namespace BlazorServer
{
    public class Startup
    {
        private IConfiguration _config { get; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddHttpContextAccessor();
            services.AddBlazoredModal();

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseMySql(_config.GetConnectionString("DevBlogDB"));
            });

            services.AddDbContext<UserDbContext>(opt =>
            {
                opt.UseMySql(_config.GetConnectionString("DevBlogUserDB"));
            });

            services.AddIdentity<UserModel, IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy => policy.RequireRole(RoleModel.Admin));
                opt.AddPolicy("Visitor", policy => policy.RequireRole(RoleModel.Visitor));
            });

            //TODO: Understand differences between all of these
            //services.AddScoped<>
            services.AddTransient<IEmailRepository, EmailRepository>();
            services.AddTransient<IVoteRepository, VoteRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }
    }
}