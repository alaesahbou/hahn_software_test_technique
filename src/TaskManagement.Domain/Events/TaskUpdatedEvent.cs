using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Events;

public record TaskUpdatedEvent(Guid TaskId, string Title, TaskPriority Priority) : INotification;
