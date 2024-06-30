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
            serviceCollection.AddTransient<IBookService, BookService>();
            serviceCollection.AddTransient<IAuthorService, AuthorService>();
            serviceCollection.AddTransient<IShelveService, ShelveService>();
            return serviceCollection;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddIdentity<UserModel, IdentityRole>(
                options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 3;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.ClaimsIdentity.RoleClaimType = "User";


                })
                   .AddEntityFrameworkStores<ProjectDbContext>()
                   .AddDefaultTokenProviders();
            return serviceCollection;
        }


    }
}
