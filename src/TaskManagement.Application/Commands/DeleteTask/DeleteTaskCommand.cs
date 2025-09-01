using MediatR;

namespace TaskManagement.Application.Commands.DeleteTask;

public record DeleteTaskCommand(Guid Id) : IRequest<bool>;
