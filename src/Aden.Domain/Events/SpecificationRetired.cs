using Aden.SharedKernal;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aden.Domain.Events;

public class SpecificationRetired: IDomainEvent
{
    public int SpecificationId { get; set; }
    
    
}

public class SpecificationRetiredHandler : INotificationHandler<SpecificationRetired>
{
    private readonly ILogger<SpecificationRetired> _logger;

    public SpecificationRetiredHandler(ILogger<SpecificationRetired> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(SpecificationRetired notification, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Retired Specification... ");
        
        return Task.CompletedTask;
    }
}