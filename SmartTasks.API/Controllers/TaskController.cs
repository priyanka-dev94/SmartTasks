using Microsoft.AspNetCore.Mvc;
using SmartTasks.API.Filters;
using SmartTasks.Application.Common;
using SmartTasks.Application.DTOs;
using SmartTasks.Application.Interfaces.Services;

namespace SmartTasks.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Retrieves tasks with server-side pagination.
        /// </summary>
        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] PaginationParams pagination)
        {
            var result = await _taskService.GetPagedAsync(pagination);
            return Ok(result);
        }


        /// <summary>
        /// Retrieves a specific task by its unique ID.
        /// </summary>
        /// <param name="id">The unique GUID of the task</param>
        /// <returns>TaskResponseDto if found</returns>
        /// <response code="200">Returns the requested task</response>
        /// <response code="404">If the task is not found</response>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        /// <summary>
        /// Creates a new task in the system.
        /// </summary>
        /// <param name="dto">The data required to create a task</param>
        /// <returns>The created task with its new ID</returns>
        /// <response code="201">Task created successfully</response>
        /// <response code="400">If the request data is invalid</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilter<TaskCreateDto>))]
        public async Task<IActionResult> CreateAsync([FromBody] TaskCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTask = await _taskService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetByIdAsync),
                new { id = createdTask.Id },
                createdTask);
        }

        /// <summary>
        /// Updates an existing task by its ID.
        /// </summary>
        /// <param name="id">The GUID of the task to update</param>
        /// <param name="dto">The updated task data</param>
        /// <returns>No content on success</returns>
        /// <response code="204">Task updated successfully</response>
        /// <response code="400">Invalid model data</response>
        /// <response code="404">If the task is not found</response>
        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilter<TaskUpdateDto>))]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] TaskUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var success = await _taskService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a task permanently from the system.
        /// </summary>
        /// <param name="id">The GUID of the task to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">Task deleted successfully</response>
        /// <response code="404">If the task is not found</response>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var success = await _taskService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }


}
