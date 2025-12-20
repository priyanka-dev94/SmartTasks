using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartTasks.API.Validators;
using SmartTasks.Application.DTOs;
using SmartTasks.Application.Interfaces.Services;
using SmartTasks.Application.Services;
using SmartTasks.Application.Validators;
using System.Reflection;

namespace TaskManagementSystem.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<ITaskService, TaskService>();

            // Validators
            services.AddScoped<IValidator<TaskCreateDto>, TaskCreateDtoValidator>();
            services.AddScoped<IValidator<TaskUpdateDto>, TaskUpdateDtoValidator>();

            return services;
        }
    }
}
