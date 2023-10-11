using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Tasks.Api.Models;
using Tasks.Api.Services;
using Tasks.Domain;
using Tasks.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Tasks.Api", Version = "v1" });
        });

        return services;
    }

    public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<User>(opt => 
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<DataContext>();

        services.Configure<JwtOptions>(opts => configuration.GetSection("JwtConfig").Bind(opts));

        var options = configuration.GetSection("JwtConfig").Get<JwtOptions>();

        if (string.IsNullOrWhiteSpace(options?.AccessTokenSecret))
            throw new Exception("Jwt secret key is missing from appsettings.json");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt => 
            {
                opt.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.AccessTokenSecret)),
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

        services.AddScoped<TokenGeneratorService>();
        services.AddScoped<RefreshTokenValidator>();
        services.AddScoped<AuthenticationService>();

        return services;
    }
}
