using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartTasks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartTasks.Infrastructure.Persistence.Configurations
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            // Primary key
            builder.HasKey(t => t.Id);

            // Index for filtering by status
            builder.HasIndex(t => t.Status);

            // Index for sorting by created date
            builder.HasIndex(t => t.CreatedOn);

            // Index for searching by title
            builder.HasIndex(t => t.Title);

            // Composite index for multi-tenant queries
            builder.HasIndex(t => new { t.UserId, t.Status });

            // Optional: constraints
            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(t => t.UserId)
                   .IsRequired();
        }
    }
}
