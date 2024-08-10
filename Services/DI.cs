using Microsoft.Extensions.DependencyInjection;
using Services.Items;

namespace Services;

public static class DI
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IItemsService, ItemsService>();
        services.AddTransient<IStoragesService, StoragesService>();
        services.AddTransient<IPropertiesService, PropertiesService>();
        services.AddTransient<ICharactersService, CharactersService>();
    }
}