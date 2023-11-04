using System.Reflection;


using FluentValidation;


using MediatR;


using Microsoft.Extensions.DependencyInjection;


using Tasks.Application.Common.Behavior;

namespace Tasks.Application.Extensions;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionRegister).Assembly));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}