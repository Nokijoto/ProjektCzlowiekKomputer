using Microsoft.AspNetCore.Identity;
using Project.Data;
using Project.Data.Models;
using ProjektCzlowiekKomputer.Interfaces;
using ProjektCzlowiekKomputer.Services;

namespace ProjektCzlowiekKomputer.Extensions
{
    public static class ServicesExtensions
    {


        public static IServiceCollection AddCustomServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthService, AuthService>();
            return serviceCollection;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<UserModel, IdentityRole>()
                   .AddEntityFrameworkStores<ProjectDbContext>()
                   .AddDefaultTokenProviders();
            return serviceCollection;
        }


    }
}
