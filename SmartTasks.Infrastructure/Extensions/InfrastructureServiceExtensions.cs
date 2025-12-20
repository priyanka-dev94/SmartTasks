using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartTasks.API.Repositories.Abstraction;
using SmartTasks.Infrastructure.Persistence;
using SmartTasks.Infrastructure.Repositories.Implementation;

namespace SmartTasks.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            // Repositories
            services.AddScoped<ITaskRepository, TaskRepository>();

            return services;
        }
    }
}
