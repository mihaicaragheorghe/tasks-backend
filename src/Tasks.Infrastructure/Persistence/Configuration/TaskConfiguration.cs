using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Tasks.Infrastructure.Persistence;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .HasMaxLength(Constants.Task.TitleMaxLength)
            .IsRequired();

        builder.Property(t => t.Description)
            .HasMaxLength(Constants.Task.DescriptionMaxLength);

        builder.HasOne(t => t.CreatedBy)
            .WithMany()
            .HasForeignKey(t => t.CreatedByUserId)
            .IsRequired();
        
        builder.Navigation(t => t.CreatedBy)
            .AutoInclude();

        builder.HasOne(t => t.AssignedTo)
            .WithMany()
            .HasForeignKey(t => t.AssignedToUserId)
            .IsRequired();

        builder.Navigation(t => t.AssignedTo)
            .AutoInclude();

        builder.HasMany(t => t.Subtasks)
            .WithOne()
            .HasForeignKey(s => s.ParentId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Navigation(t => t.Subtasks)
            .AutoInclude();

        builder.HasMany(t => t.Tags)
            .WithMany()
            .UsingEntity(j => j.ToTable("TaskTagIds"));

        builder.Navigation(t => t.Tags)
            .AutoInclude();
        
        builder.HasMany(t => t.Comments)
            .WithOne()
            .HasForeignKey(c => c.TaskId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(t => t.Comments)
            .AutoInclude();
        
        builder.HasQueryFilter(t => !t.IsDeleted);
    }
}