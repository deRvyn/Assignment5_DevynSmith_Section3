using Assignment8_DevynSmith_Section3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment8_DevynSmith_Section3
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
                options.UseSqlite(Configuration["ConnectionStrings:BooksConnection"]);
            });

            services.AddScoped<IBooksRepository, EFBooksRepository>();

            //adds razor pages functionality to our project
            services.AddRazorPages();

            //makes the razor page info stick
            services.AddDistributedMemoryCache();
            services.AddSession();

            //adding in necessary services, making the same object be used for anything cart related
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

            //adds a session for us
            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            //endpoint edited to make it look better in the URL
            app.UseEndpoints(endpoints =>
            {
                //url for category
                endpoints.MapControllerRoute("catpage",
                    "{category}/{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                //url for just putting a number
                endpoints.MapControllerRoute("page",
                    "{pageNum:int}",
                    new { Controller = "Home", action = "Index" });

                //url for just category
                endpoints.MapControllerRoute("category",
                    "{category}",
                    new { Controller = "Home", action = "Index", page = 1});

                //url for pagination
                endpoints.MapControllerRoute(
                    "pagination",
                    "Books/P{pageNum}",
                    new { Controller = "Home", action = "Index" });

                //default url (index)
                endpoints.MapDefaultControllerRoute();

                //enables routing for razor pages
                endpoints.MapRazorPages();
            });

            //makes sure the seed data populates on startup if needed
            SeedData.EnsurePopulated(app);
        }
    }
}
