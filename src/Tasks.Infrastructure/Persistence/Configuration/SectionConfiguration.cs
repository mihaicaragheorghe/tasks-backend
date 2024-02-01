using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain;

namespace Tasks.Infrastructure.Persistence;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        builder.ToTable("Sections");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name)
            .HasMaxLength(Constants.Section.NameMaxLength)
            .IsRequired();

        builder.HasMany(s => s.Tasks)
            .WithOne()
            .HasForeignKey(t => t.SectionId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);

        builder.Navigation(s => s.Tasks)
            .AutoInclude();
    }
}