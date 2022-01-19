using MediatR;
using Microsoft.Extensions.Logging;

namespace Aden.Domain.Events;

public class SampleEvent : INotification {
    public SampleEvent(string message)
    {
        Message = message;
    }

    private string Message { get; }
}

// public class Handler1 : INotificationHandler<SampleEvent>
// {
//     private readonly ILogger<Handler1> _logger;
//
//     public Handler1(ILogger<Handler1> logger)
//     {
//         _logger = logger;
//     }
//     public void Handle(SampleEvent notification)
//     {
//         _logger.LogWarning($"Handled: {notification.Message}");
//     }
//
//     public Task Handle(SampleEvent notification, CancellationToken cancellationToken)
//     {
//         _logger.LogWarning($"Handled: {notification.Message}");
//         return Task.CompletedTask;
//     }
// }
// public class Handler2 : INotificationHandler<SampleEvent>
// {
//     private readonly ILogger<Handler2> _logger;
//
//     public Handler2(ILogger<Handler2> logger)
//     {
//         _logger = logger;
//     }
//     public void Handle(SampleEvent notification)
//     {
//         _logger.LogWarning($"Handled: {notification.Message}");
//     }
//
//     public Task Handle(SampleEvent notification, CancellationToken cancellationToken)
//     {
//         _logger.LogWarning($"Handled: {notification.Message}");
//         return Task.CompletedTask;
//     }
// }