using BlazorShop.Domain.Dispatchers.Interfaces;
using BlazorShop.Domain.Repositories.Interfaces;
using BlazorShop.Infrastructure.Dispatchers;
using BlazorShop.Infrastructure.Persistence.MongoDb;
using BlazorShop.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;

namespace BlazorShop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<IMongoDbRepository, MongoDbRepository>();

        var eventStoreConnectionString = configuration.GetRequiredSection(AppSettingConstants.DatabaseSettings)[AppSettingConstants.EventStoreConnectionString];
        services.AddEventStoreClient(eventStoreConnectionString);

        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddTransient<IEventStoreRepository, EventStoreRepository>(); 

        return services;
    }
}