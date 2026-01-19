using Bilet1.DAL;
using Bilet1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bilet1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(ops=>ops.UseSqlServer(
                builder.Configuration.GetConnectionString("Default"))
            );

            builder.Services.AddIdentity<AppUser, IdentityRole>(ops =>
            {
                ops.Password.RequiredLength = 8;
                ops.User.RequireUniqueEmail = true;
                ops.Lockout.MaxFailedAccessAttempts = 3;
                ops.Lockout.AllowedForNewUsers = true;
                ops.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
            }).AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            
                    
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
