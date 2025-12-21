using Microsoft.EntityFrameworkCore;
using SmartTasks.API.Repositories.Abstraction;
using SmartTasks.Application.Common;
using SmartTasks.Domain.Entities;
using SmartTasks.Infrastructure.Persistence;

namespace SmartTasks.Infrastructure.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TaskItem>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var query = _context.TaskItems.AsNoTracking();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.CreatedOn)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                Items = items
            };
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(TaskItem item)
        {
            await _context.TaskItems.AddAsync(item);
        }

        public async Task UpdateAsync(TaskItem item)
        {
            _context.TaskItems.Update(item);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(TaskItem item)
        {
            _context.TaskItems.Remove(item);
            await Task.CompletedTask;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }

}
