using Microsoft.Extensions.DependencyInjection;

namespace Services;

public static class DI
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IItemsService, ItemsService>();
        services.AddTransient<IStoragesService, StoragesService>();
        services.AddTransient<IPropertiesService, PropertiesService>();
    }
}