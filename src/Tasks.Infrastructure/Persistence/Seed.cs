using Microsoft.AspNetCore.Identity;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence;

public class Seed
{
    public static async Task SeedData(DataContext context,
        UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                User.Create("Bob", "bob", "bob@test.com"),
                User.Create("Tom", "tom", "tom@test.com"),
                User.Create("Jane", "jane", "jane@test.com"),
            };

            context.Users.AddRange(users);

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");

                if (!context.Projects.Any(p => p.OwnerId == user.Id))
                {
                    var project = Project.Create("Inbox", user.Id);
                    var section = Section.Create(project.Id, "Getting started");
                    var task = TaskEntity.Create(
                        user.Id,
                        user.Id,
                        project.Id,
                        section.Id,
                        "Welcome to Tasks",
                        "TODO - add description",
                        TaskPriority.None);
                    var subtask = Subtask.Create(task.Id, "This is a subtask");


                    context.Projects.Add(project);
                    context.Sections.Add(section);
                    context.Tasks.Add(task);
                    context.Subtasks.Add(subtask);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}