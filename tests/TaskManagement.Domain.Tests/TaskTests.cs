using Xunit;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Events;
using DomainTask = TaskManagement.Domain.Entities.Task;
using DomainTaskStatus = TaskManagement.Domain.Entities.TaskStatus;

namespace TaskManagement.Domain.Tests;

public class TaskTests
{
    [Fact]
    public void CreateTask_WithValidData_ShouldCreateTaskWithCorrectProperties()
    {
        // Arrange
        var title = "Test Task";
        var description = "Test Description";
        var priority = TaskPriority.High;
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act
        var task = new DomainTask(title, description, priority, dueDate);

        // Assert
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.Equal(priority, task.Priority);
        Assert.Equal(dueDate, task.DueDate);
        Assert.Equal(DomainTaskStatus.Pending, task.Status);
        Assert.True(task.CreatedDate <= DateTime.UtcNow);
        Assert.Null(task.CompletedDate);
        Assert.Single(task.DomainEvents);
        Assert.IsType<TaskCreatedEvent>(task.DomainEvents.First());
    }

    [Fact]
    public void CreateTask_WithEmptyTitle_ShouldThrowArgumentException()
    {
        // Arrange
        var title = "";
        var description = "Test Description";
        var priority = TaskPriority.Medium;
        var dueDate = DateTime.UtcNow.AddDays(1);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new DomainTask(title, description, priority, dueDate));
        Assert.Equal("Title cannot be empty (Parameter 'title')", exception.Message);
    }

    [Fact]
    public void CreateTask_WithPastDueDate_ShouldThrowArgumentException()
    {
        // Arrange
        var title = "Test Task";
        var description = "Test Description";
        var priority = TaskPriority.Medium;
        var dueDate = DateTime.UtcNow.AddDays(-1);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new DomainTask(title, description, priority, dueDate));
        Assert.Equal("Due date must be in the future (Parameter 'dueDate')", exception.Message);
    }

    [Fact]
    public void StartTask_WhenPending_ShouldChangeStatusToInProgress()
    {
        // Arrange
        var task = CreateValidTask();
        var initialEventCount = task.DomainEvents.Count;

        // Act
        task.Start();

        // Assert
        Assert.Equal(DomainTaskStatus.InProgress, task.Status);
        Assert.Equal(initialEventCount + 1, task.DomainEvents.Count);
        Assert.IsType<TaskStartedEvent>(task.DomainEvents.Last());
    }

    [Fact]
    public void StartTask_WhenNotPending_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var task = CreateValidTask();
        task.Start(); // First start

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => task.Start());
        Assert.Equal("Only pending tasks can be started", exception.Message);
    }

    [Fact]
    public void CompleteTask_WhenInProgress_ShouldChangeStatusToCompleted()
    {
        // Arrange
        var task = CreateValidTask();
        task.Start();
        var eventCountBeforeComplete = task.DomainEvents.Count;

        // Act
        task.Complete();

        // Assert
        Assert.Equal(DomainTaskStatus.Completed, task.Status);
        Assert.NotNull(task.CompletedDate);
        Assert.Equal(eventCountBeforeComplete + 1, task.DomainEvents.Count);
        Assert.IsType<TaskCompletedEvent>(task.DomainEvents.Last());
    }

    [Fact]
    public void CompleteTask_WhenNotInProgress_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var task = CreateValidTask();

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => task.Complete());
        Assert.Equal("Only in-progress tasks can be completed", exception.Message);
    }

    [Fact]
    public void CancelTask_WhenNotCompleted_ShouldChangeStatusToCancelled()
    {
        // Arrange
        var task = CreateValidTask();
        var initialEventCount = task.DomainEvents.Count;

        // Act
        task.Cancel();

        // Assert
        Assert.Equal(DomainTaskStatus.Cancelled, task.Status);
        Assert.Equal(initialEventCount + 1, task.DomainEvents.Count);
        Assert.IsType<TaskCancelledEvent>(task.DomainEvents.Last());
    }

    [Fact]
    public void CancelTask_WhenCompleted_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var task = CreateValidTask();
        task.Start();
        task.Complete();

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => task.Cancel());
        Assert.Equal("Completed tasks cannot be cancelled", exception.Message);
    }

    [Fact]
    public void UpdateDetails_WithValidData_ShouldUpdateTaskProperties()
    {
        // Arrange
        var task = CreateValidTask();
        var initialEventCount = task.DomainEvents.Count;
        var newTitle = "Updated Task";
        var newDescription = "Updated Description";
        var newPriority = TaskPriority.Critical;
        var newDueDate = DateTime.UtcNow.AddDays(2);

        // Act
        task.UpdateDetails(newTitle, newDescription, newPriority, newDueDate);

        // Assert
        Assert.Equal(newTitle, task.Title);
        Assert.Equal(newDescription, task.Description);
        Assert.Equal(newPriority, task.Priority);
        Assert.Equal(newDueDate, task.DueDate);
        Assert.Equal(initialEventCount + 1, task.DomainEvents.Count);
        Assert.IsType<TaskUpdatedEvent>(task.DomainEvents.Last());
    }

    [Fact]
    public void ClearDomainEvents_ShouldRemoveAllDomainEvents()
    {
        // Arrange
        var task = CreateValidTask();
        Assert.Single(task.DomainEvents);

        // Act
        task.ClearDomainEvents();

        // Assert
        Assert.Empty(task.DomainEvents);
    }

    private static DomainTask CreateValidTask()
    {
        return new DomainTask("Test Task", "Test Description", TaskPriority.Medium, DateTime.UtcNow.AddDays(1));
    }
}
