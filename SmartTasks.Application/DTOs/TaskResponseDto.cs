namespace SmartTasks.Application.DTOs
{
    public class TaskResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTimeOffset? DueDate { get; set; }
        public string UserId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
    }
}
