using BlazorServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseMySql(_config.GetConnectionString("DevBlogDB"));
            });

            services.AddDbContext<UserDbContext>(options =>
            {
                options.UseMySql(_config.GetConnectionString("DevBlogUserDB"));
            });

            services.AddIdentity<UserModel, IdentityRole>()
                    .AddEntityFrameworkStores<UserDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole(RoleModel.Admin));
                options.AddPolicy("Visitor", policy => policy.RequireRole(RoleModel.Visitor));
            });

            //TODO: Understand differences between all of these
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<AppState>();
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