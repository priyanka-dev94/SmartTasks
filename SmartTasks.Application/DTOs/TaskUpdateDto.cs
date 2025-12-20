using System.ComponentModel.DataAnnotations;

namespace SmartTasks.Application.DTOs
{
    public class TaskUpdateDto
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        public DateTimeOffset? DueDate { get; set; }

        public TaskStatus? Status { get; set; }
    }
}
