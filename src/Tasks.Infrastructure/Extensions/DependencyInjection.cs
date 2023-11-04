using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using Tasks.Application.Common.Repository;
using Tasks.Infrastructure.Persistence;
using Tasks.Infrastructure.Persistence.Repository;

namespace Tasks.Infrastructure.Extensions;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITagRepository, TagRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ISectionRepository, SectionRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ISubtaskRepository, SubtaskRepository>();

        return services;
    }
}