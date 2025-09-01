using DomainTaskPriority = TaskManagement.Domain.Entities.TaskPriority;

namespace TaskManagement.Application.DTOs;

public record CreateTaskDto(
    string Title,
    string Description,
    DomainTaskPriority Priority,
    DateTime DueDate
);
