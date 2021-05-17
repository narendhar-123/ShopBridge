using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopBridge.IRepository;
using ShopBridge.IService;
using ShopBridge.Logger;
using ShopBridge.Repository;
using ShopBridge.Repository.Context;
using ShopBridge.Service;

namespace ShopBridge.DependencyResolver
{
    public static class DependenciesResolver
    {
        public static void ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ShopBridgeContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.InjectServices();
            services.InjectRepositories();
        }

        internal static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IInventoryService, InventoryService>();
        }

        internal static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IInventoryRepository, InventoryRepository>();
        }
    }
}
