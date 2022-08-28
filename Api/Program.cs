using Application;
using Application.Commands;
using Infrastructure;
using Pigsty.CQRS.Command;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication()
                .AddInfrastructure();

var app = builder.Build();

app.MapGet("/", () => "Identity Service");

app.MapPost("/tenants", (CancellationToken token, CreateTenantCommand command, ICommandDispatcher commandDispatcher) 
    => commandDispatcher.SendAsync(command, token));

app.MapPost("/users", (CancellationToken token, CreateUserCommand command, ICommandDispatcher commandDispatcher) 
    => commandDispatcher.SendAsync(command, token));

app.MapPost("/users/{id}/request-password-change", (CancellationToken token, RequestPasswordChangeCommand command, ICommandDispatcher commandDispatcher)
    => commandDispatcher.SendAsync(command, token));

app.MapPost("/users/{id}/recover-password", (CancellationToken token, RecoverPasswordCommand command, ICommandDispatcher commandDispatcher)
    => commandDispatcher.SendAsync(command, token));

app.UseApplication()
   .UseInfrastructure();

app.Run();
