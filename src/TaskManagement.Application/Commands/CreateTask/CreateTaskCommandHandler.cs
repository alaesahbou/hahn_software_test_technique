using MediatR;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using DomainTask = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Commands.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPublisher _publisher;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
    {
        _taskRepository = taskRepository;
        _publisher = publisher;
    }

    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new DomainTask(
            request.CreateTaskDto.Title,
            request.CreateTaskDto.Description,
            request.CreateTaskDto.Priority,
            request.CreateTaskDto.DueDate
        );

        var createdTask = await _taskRepository.AddAsync(task);

        // Publish domain events
        foreach (var domainEvent in task.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        task.ClearDomainEvents();

        return new TaskDto(
            createdTask.Id,
            createdTask.Title,
            createdTask.Description,
            createdTask.Status,
            createdTask.Priority,
            createdTask.DueDate,
            createdTask.CreatedDate,
            createdTask.CompletedDate
        );
    }
}
