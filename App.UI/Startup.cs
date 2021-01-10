using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Interfaces;
using App.Infrastructure.Context;
using App.Infrastructure.Filters;
using App.Infrastructure.Repository;
using App.Infrastructure.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using App.Infrastructure.Validators;
using FluentValidation;
using App.Core.Services;

namespace App.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //Disable localization manager so we can work with default english messages
            ValidatorOptions.Global.LanguageManager.Enabled = false;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShoppingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Shopping")));
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeTypeService, EmployeeTypeService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.Configure<ExceptionFilterOptions>(Configuration.GetSection("ExceptionFilter"));
            services.AddControllersWithViews(options => {
                options.Filters.Add<CustomExceptionFilter>();
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmployeeValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
