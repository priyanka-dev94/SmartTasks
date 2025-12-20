using System.ComponentModel.DataAnnotations;

namespace SmartTasks.Application.DTOs
{
    public class TaskCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        // Optional due date
        public DateTimeOffset? DueDate { get; set; }
    }
}
