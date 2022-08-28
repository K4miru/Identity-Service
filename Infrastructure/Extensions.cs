using Application.Repositories;
using Application.Services;
using Infrastructure.Repositories.Tenants;
using Infrastructure.Repositories.Users;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pigsty.Databases.MongoDb;
using Pigsty.MessagesBrokers;
using Pigsty.Documentation;
using Pigsty.HealthChecks;

namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMongoDb()
                .AddMessageBroker()
                .AddAllEventSubscribers()
                .AddDocumentation()
                .AddAllHealthChecks();

        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMongoDb()
           .UseDocumentation()
           .UseAllHealthChecks();
        return app;
    }
}