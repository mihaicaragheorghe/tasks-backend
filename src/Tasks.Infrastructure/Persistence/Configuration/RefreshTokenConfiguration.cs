using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Application.Common.Models;
using Domain;

namespace Tasks.Infrastructure.Persistence;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(t => t.UserId);

        builder.HasOne<User>()
            .WithOne()
            .HasForeignKey<RefreshToken>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(t => t.Token)
            .HasMaxLength(200)
            .IsRequired();
    }
}