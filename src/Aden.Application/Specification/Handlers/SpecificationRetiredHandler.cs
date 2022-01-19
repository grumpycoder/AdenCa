using Aden.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Aden.Application.Specification.Handlers;

public class SpecificationRetiredEmailHandler : INotificationHandler<SpecificationWasRetired>
{
    private readonly ILogger<SpecificationWasRetired> _logger;

    public SpecificationRetiredEmailHandler(ILogger<SpecificationWasRetired> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(SpecificationWasRetired notification, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Handled: Retired {notification.FileName}({notification.FileNumber}) Specification from Event");
        //TODO: Send email
        return Task.CompletedTask;
    }
}