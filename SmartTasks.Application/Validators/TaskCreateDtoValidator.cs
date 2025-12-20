using FluentValidation;
using SmartTasks.Application.DTOs;

namespace SmartTasks.Application.Validators
{
    public class TaskCreateDtoValidator : AbstractValidator<TaskCreateDto>
    {
        public TaskCreateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.DueDate.HasValue)
                .WithMessage("Due date must be in the future.");
        }
    }
}
