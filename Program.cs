using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Context;
using PerformanceSurvey.Controllers;
using PerformanceSurvey.Models;
using System;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PerformanceSurvey
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllersWithViews()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    // Optional: Configure other settings if needed
                    // options.JsonSerializerOptions.MaxDepth = 64;
                });

            // Register UserController as a scoped service
            builder.Services.AddScoped<UserController>();

            var app = builder.Build();

            // Ensure database is created and apply migrations
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.MigrateAsync();
            }

            // Seed initial data or perform other startup tasks
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userController = services.GetRequiredService<UserController>();

                var newUser = new User
                {
                    name = "Our Admin",
                    userEmail = "admin@example.com",
                    password = "password",
                    userType = UserType.AdminUser,
                    createdAt = DateTime.UtcNow
                };

                await userController.EnsureUserExistsAsync(newUser.userEmail, newUser);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios.
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
