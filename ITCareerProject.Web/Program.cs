using AutoMapper;
using GlobalConstants.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ITCareerProject.Data;
using ITCareerProject.Services.EventsService;
using ITCareerProject.Services.RolesService;
using ITCareerProject.Services.SeederService;
using ITCareerProject.Services.TicketsService;
using ITCareerProject.Services.UsersService;

namespace ITCareerProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
                .AddRoles<IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            builder.Services.AddSingleton(builder.Configuration);
            builder.Services.AddScoped<ApplicationDbContext>();

            builder.Services.AddTransient<ISeederService, SeederService>();
            builder.Services.AddTransient<IUsersService, UsersService>();
            builder.Services.AddTransient<IRolesService, RolesService>();
            builder.Services.AddTransient<IEventsService, EventsService>();
            builder.Services.AddTransient<ITicketsService, TicketsService>();

            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
             
                new SeederService(dbContext, serviceScope.ServiceProvider).InitiateSeed().GetAwaiter().GetResult();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();


        }
    }
}