using FluentValidation;
using SmartTasks.API.Filters;
using SmartTasks.API.Repositories.Abstraction;
using SmartTasks.API.Validators;
using SmartTasks.Application.DTOs;
using SmartTasks.Application.Validators;
using SmartTasks.Infrastructure.Repositories.Implementation;
using System.Reflection;

namespace SmartTasks.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection AddValidationServices(this IServiceCollection services)
        {
            // Register validators
            services.AddScoped<IValidator<TaskCreateDto>, TaskCreateDtoValidator>();
            services.AddScoped<IValidator<TaskUpdateDto>, TaskUpdateDtoValidator>();

            // Register filters
            services.AddScoped<ValidationFilter<TaskCreateDto>>();
            services.AddScoped<ValidationFilter<TaskUpdateDto>>();

            return services;
        }
    }
}
            
