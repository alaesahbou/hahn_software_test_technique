using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Commands.CompleteTask;

public record CompleteTaskCommand(Guid Id) : IRequest<TaskDto>;
