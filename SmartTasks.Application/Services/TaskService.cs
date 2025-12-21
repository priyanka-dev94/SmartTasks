using AutoMapper;
using SmartTasks.API.Repositories.Abstraction;
using SmartTasks.Application.Common;
using SmartTasks.Application.DTOs;
using SmartTasks.Application.Interfaces.Services;
using SmartTasks.Domain.Entities;

namespace SmartTasks.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PagedResult<TaskResponseDto>> GetPagedAsync(PaginationParams pagination)
        {
            var result = await _repo.GetPagedAsync(
                pagination.PageNumber,
                pagination.PageSize);

            return new PagedResult<TaskResponseDto>
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize,
                TotalCount = result.TotalCount,
                Items = _mapper.Map<IReadOnlyList<TaskResponseDto>>(result.Items)
            };
        }

        public async Task<TaskResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            return _mapper.Map<TaskResponseDto?>(item);
        }

        public async Task<TaskResponseDto> CreateAsync(TaskCreateDto dto)
        {
            var entity = _mapper.Map<TaskItem>(dto);
            entity.Id = Guid.NewGuid();

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<TaskResponseDto>(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, TaskUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            await _repo.UpdateAsync(entity);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            await _repo.DeleteAsync(entity);
            return await _repo.SaveChangesAsync();
        }
    }
}
