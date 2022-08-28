using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pigsty.CQRS;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCQRS()
                .AddAllCommandHandlers()
                .AddAllQueryHandlers();

        return services;
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        return app;
    }
}