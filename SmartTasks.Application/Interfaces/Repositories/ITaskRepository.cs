using SmartTasks.Application.Common;
using SmartTasks.Domain.Entities;
namespace SmartTasks.API.Repositories.Abstraction
{
    public interface ITaskRepository
    {
        Task<PagedResult<TaskItem>> GetPagedAsync(TaskQueryParams query);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task AddAsync(TaskItem item);
        Task UpdateAsync(TaskItem item);
        Task DeleteAsync(TaskItem item);
        Task<bool> SaveChangesAsync();
    }

}
