using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SmartTasks.API.Filters
{
    public class ValidationFilter<T> : IAsyncActionFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("dto", out var value) && value is T model)
            {
                var result = await _validator.ValidateAsync(model);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                        .Select(e => new { e.PropertyName, e.ErrorMessage });

                    context.Result = new BadRequestObjectResult(errors);
                    return;
                }
            }

            await next();
        }
    }
}
