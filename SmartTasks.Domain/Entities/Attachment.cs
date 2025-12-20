using System.ComponentModel.DataAnnotations;

namespace SmartTasks.Domain.Entities
{
    public class Attachment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(260)]
        public string FileName { get; set; } = null!;

        // Local path in Phase1 or Blob URL in Phase2
        [Required]
        public string Url { get; set; } = null!;

        public long? SizeBytes { get; set; }

        public DateTimeOffset UploadedOn { get; set; } = DateTimeOffset.UtcNow;

        // Foreign key
        [Required]
        public Guid TaskItemId { get; set; }

        // Navigation
        public virtual TaskItem? TaskItem { get; set; }
    }
}
