using MediatR;

namespace TaskManagement.Domain.Events;

public record TaskCompletedEvent(Guid TaskId, string Title, DateTime CompletedDate) : INotification;
