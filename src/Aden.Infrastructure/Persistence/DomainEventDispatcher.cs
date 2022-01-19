using Aden.SharedKernal;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aden.Infrastructure.Persistence;

public class DomainEventDispatcher : SaveChangesInterceptor
{
    private readonly IMediator _mediator;
    
    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        return SavingChangesAsync(eventData, result).GetAwaiter().GetResult();
    }
    
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await DispatchDomainEventsAsync(eventData.Context.ChangeTracker);
        return result;
    }
    private async Task DispatchDomainEventsAsync(ChangeTracker changeTracker)
    {
        var domainEntities = changeTracker
            .Entries<DomainEntity>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Any())
            .ToList();
    
        foreach (var entity in domainEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}