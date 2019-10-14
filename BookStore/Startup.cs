using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BookStore.ModelBinders;
using BookStore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
      services.AddMvc(options => options.EnableEndpointRouting = false);

      services.AddDbContextPool<AppDbContext>(
        options => options.UseSqlServer(_config.GetConnectionString("BookstoreDBConnection")));

      services.AddSingleton<IBookRepository, MockBookRepository>();

      services.AddMvc(options => options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider()));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseRequestLocalization(new RequestLocalizationOptions
      //{
      //  DefaultRequestCulture = new RequestCulture(new CultureInfo("pl-PL")),
      //  SupportedCultures = new List<CultureInfo>
      //  {
      //      new CultureInfo("pl-PL")
      //  },
      //  SupportedUICultures = new List<CultureInfo>
      //  {
      //      new CultureInfo("pl")
      //  }
      //});

      app.UseStaticFiles();

      //app.UseMvcWithDefaultRoute();
      app.UseMvc(route => route.MapRoute("default", "{controller=Book}/{action=Index}/{id?}"));

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
