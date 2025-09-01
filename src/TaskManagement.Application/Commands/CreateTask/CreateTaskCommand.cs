using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Commands.CreateTask;

public record CreateTaskCommand(CreateTaskDto CreateTaskDto) : IRequest<TaskDto>;
