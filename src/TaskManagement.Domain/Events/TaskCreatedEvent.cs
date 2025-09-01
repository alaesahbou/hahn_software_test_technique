using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Events;

public record TaskCreatedEvent(Guid TaskId, string Title, TaskPriority Priority) : INotification;
