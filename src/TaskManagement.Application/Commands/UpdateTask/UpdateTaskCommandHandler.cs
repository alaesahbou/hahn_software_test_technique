using MediatR;
using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Repositories;

namespace TaskManagement.Application.Commands.UpdateTask;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IPublisher _publisher;

    public UpdateTaskCommandHandler(ITaskRepository taskRepository, IPublisher publisher)
    {
        _taskRepository = taskRepository;
        _publisher = publisher;
    }

    public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
            throw new InvalidOperationException($"Task with ID {request.Id} not found");

        task.UpdateDetails(
            request.UpdateTaskDto.Title,
            request.UpdateTaskDto.Description,
            request.UpdateTaskDto.Priority,
            request.UpdateTaskDto.DueDate
        );

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
