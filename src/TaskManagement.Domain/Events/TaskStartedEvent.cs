using MediatR;

namespace TaskManagement.Domain.Events;

public record TaskStartedEvent(Guid TaskId, string Title) : INotification;
