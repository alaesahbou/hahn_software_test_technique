using DomainTaskStatus = TaskManagement.Domain.Entities.TaskStatus;
using DomainTaskPriority = TaskManagement.Domain.Entities.TaskPriority;

namespace TaskManagement.Application.DTOs;

public record TaskDto(
    Guid Id,
    string Title,
    string Description,
    DomainTaskStatus Status,
    DomainTaskPriority Priority,
    DateTime DueDate,
    DateTime CreatedDate,
    DateTime? CompletedDate
);
