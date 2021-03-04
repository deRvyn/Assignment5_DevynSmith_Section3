using Assignment7_DevynSmith_Section3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment7_DevynSmith_Section3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //adds needed services to make the database work
            services.AddDbContext<BooksDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:BooksConnection"]);
            });

            services.AddScoped<IBooksRepository, EFBooksRepository>();
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

            //endpoint edited to make it look better in the URL
            app.UseEndpoints(endpoints =>
            {
                //url for category
                endpoints.MapControllerRoute("catpage",
                    "{category}/{page:int}",
                    new { Controller = "Home", action = "Index" });

                //url for just putting a number
                endpoints.MapControllerRoute("page",
                    "{page:int}",
                    new { Controller = "Home", action = "Index" });

                //url for just category
                endpoints.MapControllerRoute("category",
                    "{category}",
                    new { Controller = "Home", action = "Index", page = 1});

                //url for pagination
                endpoints.MapControllerRoute(
                    "pagination",
                    "Books/P{page}",
                    new { Controller = "Home", action = "Index" });

                //default url (index)
                endpoints.MapDefaultControllerRoute();
            });

            //makes sure the seed data populates on startup if needed
            SeedData.EnsurePopulated(app);
        }
    }
}
