using System.ComponentModel.DataAnnotations;
using SmartTasks.Domain.Enums;

namespace SmartTasks.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(2000)]
        public string? Description { get; set; }

        // Use DateTimeOffset for timezone-aware times
        public DateTimeOffset? DueDate { get; set; }

        [Required]
        public string UserId { get; set; } = "user123"; // temporary fake user in Phase1

        [Required]
        public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.Pending;

        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? UpdatedOn { get; set; }

        // Navigation property for attachments (one-to-many)
        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
