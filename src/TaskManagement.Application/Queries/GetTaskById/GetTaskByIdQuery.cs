using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Queries.GetTaskById;

public record GetTaskByIdQuery(Guid Id) : IRequest<TaskDto?>;
