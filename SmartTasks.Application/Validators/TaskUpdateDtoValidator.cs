using FluentValidation;
using SmartTasks.Application.DTOs;

namespace SmartTasks.API.Validators
{
    public class TaskUpdateDtoValidator : AbstractValidator<TaskUpdateDto>
    {
        public TaskUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(1000);

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value.");

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.DueDate.HasValue)
                .WithMessage("Due date must be in the future.");
        }
    }
}
