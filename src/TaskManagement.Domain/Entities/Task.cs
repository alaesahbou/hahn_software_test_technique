using System;
using System.Collections.Generic;
using MediatR;
using TaskManagement.Domain.Events;

namespace TaskManagement.Domain.Entities;

public class Task : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? CompletedDate { get; private set; }
    
    private readonly List<INotification> _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    private Task() { } // For EF Core

    public Task(string title, string description, TaskPriority priority, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (dueDate <= DateTime.UtcNow)
            throw new ArgumentException("Due date must be in the future", nameof(dueDate));

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        Status = TaskStatus.Pending;
        CreatedDate = DateTime.UtcNow;
        
        AddDomainEvent(new TaskCreatedEvent(Id, Title, Priority));
    }

    public void UpdateDetails(string title, string description, TaskPriority priority, DateTime dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        if (dueDate <= DateTime.UtcNow)
            throw new ArgumentException("Due date must be in the future", nameof(dueDate));

        Title = title;
        Description = description;
        Priority = priority;
        DueDate = dueDate;
        
        AddDomainEvent(new TaskUpdatedEvent(Id, Title, Priority));
    }

    public void Start()
    {
        if (Status != TaskStatus.Pending)
            throw new InvalidOperationException("Only pending tasks can be started");

        Status = TaskStatus.InProgress;
        AddDomainEvent(new TaskStartedEvent(Id, Title));
    }

    public void Complete()
    {
        if (Status != TaskStatus.InProgress)
            throw new InvalidOperationException("Only in-progress tasks can be completed");

        Status = TaskStatus.Completed;
        CompletedDate = DateTime.UtcNow;
        AddDomainEvent(new TaskCompletedEvent(Id, Title, CompletedDate.Value));
    }

    public void Cancel()
    {
        if (Status == TaskStatus.Completed)
            throw new InvalidOperationException("Completed tasks cannot be cancelled");

        Status = TaskStatus.Cancelled;
        AddDomainEvent(new TaskCancelledEvent(Id, Title));
    }

    private void AddDomainEvent(INotification domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed,
    Cancelled
}

public enum TaskPriority
{
    Low,
    Medium,
    High,
    Critical
}
