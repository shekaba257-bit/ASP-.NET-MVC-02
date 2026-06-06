using DemoAsp.Net8_Session01_.Contexts;
using GymManagment.DAL.Repostories.Classes;
using GymManagment.DAL.Repostories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoAsp.Net8_Session01_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Regester ==> (DI)=>Dependancy Injection
            builder.Services.AddScoped<IPlanRepostory, PlanRepostory>();
            //Ef Core Will Create Object From DbContext Automatica When We Request it From The Container (Depency Injection )
            builder.Services.AddDbContext<GymDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

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
