using MediatR;
using TaskManagement.Application.DTOs;
using DomainTaskStatus = TaskManagement.Domain.Entities.TaskStatus;

namespace TaskManagement.Application.Queries.GetTasksByStatus;

public record GetTasksByStatusQuery(DomainTaskStatus Status) : IRequest<IEnumerable<TaskDto>>;
