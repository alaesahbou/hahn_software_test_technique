using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Commands.CreateTask;
using TaskManagement.Application.Commands.UpdateTask;
using TaskManagement.Application.Commands.StartTask;
using TaskManagement.Application.Commands.CompleteTask;
using TaskManagement.Application.Commands.CancelTask;
using TaskManagement.Application.Commands.DeleteTask;
using TaskManagement.Application.Queries.GetAllTasks;
using TaskManagement.Application.Queries.GetTaskById;
using TaskManagement.Application.Queries.GetTasksByStatus;
using TaskManagement.Application.DTOs;
using DomainTaskStatus = TaskManagement.Domain.Entities.TaskStatus;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks()
    {
        var query = new GetAllTasksQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskDto>> GetTaskById(Guid id)
    {
        var query = new GetTaskByIdQuery(id);
        var result = await _mediator.Send(query);
        
        if (result == null)
            return NotFound();
            
        return Ok(result);
    }

    [HttpGet("status/{status}")]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasksByStatus(DomainTaskStatus status)
    {
        var query = new GetTasksByStatusQuery(status);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        var command = new CreateTaskCommand(createTaskDto);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTaskById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TaskDto>> UpdateTask(Guid id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var command = new UpdateTaskCommand(id, updateTaskDto);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{id:guid}/start")]
    public async Task<ActionResult<TaskDto>> StartTask(Guid id)
    {
        var command = new StartTaskCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{id:guid}/complete")]
    public async Task<ActionResult<TaskDto>> CompleteTask(Guid id)
    {
        var command = new CompleteTaskCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<ActionResult<TaskDto>> CancelTask(Guid id)
    {
        var command = new CancelTaskCommand(id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteTask(Guid id)
    {
        var command = new DeleteTaskCommand(id);
        var result = await _mediator.Send(command);
        
        if (!result)
            return NotFound();
            
        return NoContent();
    }
}
