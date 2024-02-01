using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Extensions;
using Domain;
using Tasks.Infrastructure.Extensions;
using Tasks.Infrastructure.Persistence;
using Tasks.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration)
    .AddIdentity(builder.Configuration)
    .AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
