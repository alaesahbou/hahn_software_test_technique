using MediatR;

namespace TaskManagement.Domain.Events;

public record TaskCancelledEvent(Guid TaskId, string Title) : INotification;
