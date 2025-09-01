using TaskManagement.Domain.Entities;
using DomainTask = TaskManagement.Domain.Entities.Task;
using DomainTaskStatus = TaskManagement.Domain.Entities.TaskStatus;

namespace TaskManagement.Domain.Repositories;

public interface ITaskRepository
{
    System.Threading.Tasks.Task<DomainTask?> GetByIdAsync(Guid id);
    System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetAllAsync();
    System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetByStatusAsync(DomainTaskStatus status);
    System.Threading.Tasks.Task<IEnumerable<DomainTask>> GetByPriorityAsync(TaskPriority priority);
    System.Threading.Tasks.Task<DomainTask> AddAsync(DomainTask task);
    System.Threading.Tasks.Task<DomainTask> UpdateAsync(DomainTask task);
    System.Threading.Tasks.Task DeleteAsync(Guid id);
    System.Threading.Tasks.Task<bool> ExistsAsync(Guid id);
}
