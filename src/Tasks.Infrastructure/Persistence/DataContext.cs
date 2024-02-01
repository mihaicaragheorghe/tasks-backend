using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Application.Common.Models;
using Domain;

namespace Tasks.Infrastructure.Persistence;

public class DataContext(DbContextOptions options) 
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<Section> Sections { get; set; } = null!;
    public DbSet<TaskEntity> Tasks { get; set; } = null!;
    public DbSet<Subtask> Subtasks { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}