using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartTasks.API.Repositories.Abstraction;
using SmartTasks.Application.Common;
using SmartTasks.Application.Common.Exceptions;
using SmartTasks.Application.DTOs;
using SmartTasks.Application.Interfaces.Services;
using SmartTasks.Domain.Entities;
using System.Threading.Tasks;

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

        public async Task<PagedResult<TaskResponseDto>> GetPagedAsync(TaskQueryParams queryParams)
        {
            var result = await _repo.GetPagedAsync(queryParams);

            return new PagedResult<TaskResponseDto>
            {
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount,
                Items = _mapper.Map<IReadOnlyList<TaskResponseDto>>(result.Items)
            };
        }

        public async Task<TaskResponseDto?> GetByIdAsync(Guid id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null)
                throw new NotFoundException($"Task with id '{id}' was not found.");
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

        public async Task UpdateAsync(Guid id, TaskUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Task with id '{id}' was not found.");

            _mapper.Map(dto, entity);

            try
            {
                await _repo.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ConflictException(
                    "Update failed due to a data conflict.", ex);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new NotFoundException($"Task with id '{id}' was not found.");

            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
