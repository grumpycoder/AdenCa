using Aden.Domain.Interfaces;

namespace Aden.Domain.Common;

public abstract class DomainEntity
{
    public int Id { get; set; }

    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvents(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    protected void RemoveDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
    
}