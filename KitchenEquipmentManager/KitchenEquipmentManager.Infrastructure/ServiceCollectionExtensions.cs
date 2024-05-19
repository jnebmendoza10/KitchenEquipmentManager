using KitchenEquipmentManager.Domain.Models;
using KitchenEquipmentManager.Infrastructure.Services.Equipments;
using KitchenEquipmentManager.Infrastructure.Services.Sites;
using KitchenEquipmentManager.Infrastructure.Services.Users;
using KitchenEquipmentManager.Repository;
using KitchenEquipmentManager.Repository.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KitchenEquipmentManager.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {

            // Register Repositories
            services.AddSingleton<IKitchenManagerDbContextFactory, KitchenManagerDbContextFactory>();

            services.AddSingleton<IDataRepository<User>, DataRepository<User>>();
            services.AddSingleton<IDataRepository<Site>, DataRepository<Site>>();
            services.AddSingleton<IDataRepository<Equipment>, DataRepository<Equipment>>();

            // Register Services

            //User
            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUserService, UserService>();

            //Equipment
            services.AddSingleton<IEquipmentService, EquipmentService>();   

            //Site
            services.AddSingleton<ISiteService, SiteService>();

            return services;
        }
    }
}
