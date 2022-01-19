using Aden.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aden.Application.Specification.Handlers;

public class SpecificationActivatedEmailHandler: INotificationHandler<SpecificationWasActivated>
{
    private readonly ILogger<SpecificationWasActivated> _logger;

    public SpecificationActivatedEmailHandler(ILogger<SpecificationWasActivated> logger)
    {
        _logger = logger;
    }
    
    public async Task Handle(SpecificationWasActivated notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Handled: Activated {notification.FileName}({notification.FileNumber}) Specification from Event");
    }
}