using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Commands.StartTask;

public record StartTaskCommand(Guid Id) : IRequest<TaskDto>;
