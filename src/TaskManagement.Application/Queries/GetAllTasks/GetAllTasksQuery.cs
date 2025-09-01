using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Queries.GetAllTasks;

public record GetAllTasksQuery : IRequest<IEnumerable<TaskDto>>;
