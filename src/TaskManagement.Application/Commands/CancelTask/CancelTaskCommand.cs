using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Commands.CancelTask;

public record CancelTaskCommand(Guid Id) : IRequest<TaskDto>;
