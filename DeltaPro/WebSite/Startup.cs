using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.DataBase;
using DAL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DAL.Repositories;
using Models;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using WebSite.Intrfaces;
using WebSite.Service;

namespace WebSite
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebSiteDBContext>(options => {
                options.UseSqlite("Data Source = Delta.db");
            }) ;
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<User>, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserModelService, UserModelService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton<IGlobalCart, GlobalCartService>();
            
            services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", conifg =>
                {
                    conifg.Cookie.Name = "UserCookie";
                                      
                });
            services.AddMvc(options=> options.EnableEndpointRouting=false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.UseMvcWithDefaultRoute();

        
        }
    }
}
