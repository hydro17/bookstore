using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.ModelBinders;
using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BookStore.Models.Carts;
using BookStore.Models.Books;
using BookStore.Models.OrderItems;
using BookStore.Models.Orders;
using Microsoft.AspNetCore.Mvc;

namespace BookStore
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
              options => options.UseSqlServer(_config.GetConnectionString("BookstoreDBConnection")));

            services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICartRepository, SessionCartRepository>();

            //services.AddSingleton<IBookRepository, MockBookRepository>();
            services.AddScoped<IBookRepository, SqlBookRepository>();

            //services.AddSingleton<IOrderItemRepository, MockOrderItemRepository>();
            services.AddScoped<IOrderItemRepository, SqlOrderItemRepository>();

            //services.AddSingleton<IOrderRepository, MockOrderRepository>();
            services.AddScoped<IOrderRepository, SqlOrderRepository>();

            services.AddMvc(options => options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider()));

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });
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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
              new CultureInfo("en-US"),
              new CultureInfo("pl-PL")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                // Used when no RequestCultureProvider successfully determined the request culture
                DefaultRequestCulture = new RequestCulture("en-US"),

                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseSession();

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route => route.MapRoute(name: "default", template: "{controller=Book}/{action=Index}/{id?}"));

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //  endpoints.MapGet("/", async context =>
            //        {
            //      await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
