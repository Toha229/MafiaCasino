using DAL.Models;
using DAL;
using Microsoft.AspNetCore.Identity;
using BusinessLogic.Services;

namespace MafiaCasinoWeb.Infrastructure.Services
{
    public class ServicesConfiguration
    {
        public static void Config(IServiceCollection services)
        {
			// Add services to the container.
			services.AddControllersWithViews();

            // Add razor pages
            services.AddRazorPages();

            services.AddTransient<UserService>();
            // Add application database context
            services.AddDbContext<AppDbContext>();

			// Add Identity
			services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Home/SignIn";
			});
		}
    }
}
