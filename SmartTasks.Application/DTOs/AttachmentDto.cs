namespace SmartTasks.Application.DTOs
{
    public class AttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public string Url { get; set; } = null!;
        public long? SizeBytes { get; set; }
        public DateTimeOffset UploadedOn { get; set; }
    }
}
