using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Factory.Models;
using Microsoft.AspNetCore.Identity;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<FactoryContext>(
                              dbContextOptions => dbContextOptions
                                .UseMySql(
                                  builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                                )
                              )
                            );

            // Configure Program CS for Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                      .AddEntityFrameworkStores<FactoryContext>()
                      .AddDefaultTokenProviders();


            WebApplication app = builder.Build();

            // app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Configure Program CS for Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}