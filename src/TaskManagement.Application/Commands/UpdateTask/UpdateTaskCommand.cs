using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Commands.UpdateTask;

public record UpdateTaskCommand(Guid Id, UpdateTaskDto UpdateTaskDto) : IRequest<TaskDto>;
