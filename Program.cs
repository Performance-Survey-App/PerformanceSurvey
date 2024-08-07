using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using System.Text.Json.Serialization;

namespace PerformanceSurvey
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.


            //Register ApplicationDbContext

            builder.Services.AddDbContext<ApplicationDbContext>(Options =>

            {

                Options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));

            });


            // Configure JSON serialization options
            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    // Optional: Configure other settings if needed
                    // options.JsonSerializerOptions.MaxDepth = 64;
                });

            //end

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
