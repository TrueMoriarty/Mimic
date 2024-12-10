using Microsoft.Extensions.DependencyInjection;
using Services.Items;
using Services.ItemProperties;

namespace Services;

public static class DI
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUsersService, UsersService>();
        services.AddTransient<IItemsService, ItemsService>();
        services.AddTransient<IStoragesService, StoragesService>();
        services.AddTransient<IItemPropertiesService, ItemPropertiesService>();
        services.AddTransient<ICharactersService, CharactersService>();
        services.AddTransient<IFileStorageService, S3FileStorageService>();
        services.AddTransient<IAttachedFileService, AttachedFileService>();
    }
}