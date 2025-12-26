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

        public async Task<PagedResult<TaskItem>> GetPagedAsync(TaskQueryParams query)
        {
            IQueryable<TaskItem> tasks = _context.TaskItems.AsNoTracking();

            // Filtering: Status
            if (query.Status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == query.Status.Value);
            }

            // Filtering: Search
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                tasks = tasks.Where(t =>
                    t.Title.Contains(query.Search) ||
                    (t.Description != null && t.Description.Contains(query.Search)));
            }
            // Sorting
            tasks = query.SortBy.ToLower() switch
            {
                "title" => query.IsDescending
                    ? tasks.OrderByDescending(t => t.Title)
                    : tasks.OrderBy(t => t.Title),

                "createdat" => query.IsDescending
                    ? tasks.OrderByDescending(t => t.CreatedOn)
                    : tasks.OrderBy(t => t.CreatedOn),

                _ => tasks.OrderByDescending(t => t.CreatedOn)
            };

            var totalCount = await tasks.CountAsync();

            var items = await tasks
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                PageNumber = query.PageNumber,
                PageSize = query.PageSize,
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
