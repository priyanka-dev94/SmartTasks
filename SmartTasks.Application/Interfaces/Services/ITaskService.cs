using SmartTasks.Application.Common;
using SmartTasks.Application.DTOs;

namespace SmartTasks.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<PagedResult<TaskResponseDto>> GetPagedAsync(PaginationParams pagination);
        Task<TaskResponseDto?> GetByIdAsync(Guid id);
        Task<TaskResponseDto> CreateAsync(TaskCreateDto dto);
        Task<bool> UpdateAsync(Guid id, TaskUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }


}
