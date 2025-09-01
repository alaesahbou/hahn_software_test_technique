using MediatR;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;
using DomainTask = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Application.Commands.CompleteTask;

public class CompleteTaskCommandHandler : IRequestHandler<CompleteTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPublisher _publisher;

    public CompleteTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
    {
        _taskRepository = taskRepository;
        _publisher = publisher;
    }

    public async Task<TaskDto> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
            throw new InvalidOperationException($"Task with ID {request.Id} not found");

        task.Complete();

        var updatedTask = await _taskRepository.UpdateAsync(task);

        // Publish domain events
        foreach (var domainEvent in task.DomainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        task.ClearDomainEvents();

        return new TaskDto(
            updatedTask.Id,
            updatedTask.Title,
            updatedTask.Description,
            updatedTask.Status,
            updatedTask.Priority,
            updatedTask.DueDate,
            updatedTask.CreatedDate,
            updatedTask.CompletedDate
        );
    }
}
