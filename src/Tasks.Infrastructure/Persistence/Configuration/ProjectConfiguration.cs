using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Tasks.Infrastructure.Persistence;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(Constants.Project.NameMaxLength)
            .IsRequired();

        builder.Property(p => p.Color)
            .HasMaxLength(7);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(p => p.OwnerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(p => p.Sections)
            .WithOne()
            .HasForeignKey(s => s.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(p => p.Sections)
            .AutoInclude();

        builder.HasMany(p => p.Tasks)
            .WithOne()
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Navigation(p => p.Tasks)
            .AutoInclude();
        
        builder.Ignore(p => p.Collaborators);
    }
}