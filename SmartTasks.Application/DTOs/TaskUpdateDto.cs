using SmartTasks.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Domain.Enums.TaskStatus? Status { get; set; }
    }
}
