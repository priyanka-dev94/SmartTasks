using SmartTasks.Domain.Enums;
namespace SmartTasks.Application.Common
{
    public class TaskQueryParams : PaginationParams
    {
        public Domain.Enums.TaskStatus? Status { get; set; }

        public string? Search { get; set; }

        public string SortBy { get; set; } = "createdAt";

        public bool IsDescending { get; set; } = true;
    }
}
