using Application.Events;
using Application.Exceptions;
using Application.Repositories;
using Domain.Tenants;
using Pigsty.CQRS.Command;
using Pigsty.Domain;
using Pigsty.MessagesBrokers;

namespace Application.Commands;

public record class CreateTenantCommand(string Email) : ICommand { }
internal record class CreateTenantCommandHandler(
    ITenantRepository _tenantRepository,
    IMessageBroker _messageBroker) : ICommandHandler<CreateTenantCommand>
{
    public async Task HandleAsync(CreateTenantCommand command, CancellationToken cancellationToken)
    {
        var tenant = await _tenantRepository.GetByEmail(command.Email);
        if (tenant is { })
        {
            throw new EmailAlreadyInUseException(command.Email);
        }

        tenant = Tenant.CreateTenant(new AggregateId(), command.Email);

        await _tenantRepository.AddAsync(tenant);
        await _messageBroker.Publish(new TenantCreatedEvent(tenant.Id, tenant.CreationDate));
    }
}