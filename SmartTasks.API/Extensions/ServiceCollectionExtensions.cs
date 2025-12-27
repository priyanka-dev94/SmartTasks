using FluentValidation;
using SmartTasks.API.Validators;
using SmartTasks.Application.DTOs;
using SmartTasks.Application.Validators;
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

            services.AddValidatorsFromAssemblyContaining<TaskCreateDtoValidator>();
            return services;
        }

        public static IServiceCollection AddCORSPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            return services;
        }
    }
}
