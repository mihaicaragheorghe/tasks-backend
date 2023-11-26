using FluentAssertions;

using Tasks.Application.TaskSections.Commands;
using Tasks.Domain;

namespace Tasks.Application.UnitTests.TestUtils.Sections;

public static partial class SectionExtensions
{
    public static void ValidateCreatedFrom(this Section section, CreateSectionCommand command)
    {
        section.Name.Should().Be(command.Name);
        section.ProjectId.Should().Be(command.ProjectId);
        section.Tasks.Should().BeEmpty();
        section.OrderIndex.Should().Be(0);
        section.Id.Should().NotBeEmpty();
        section.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    public static void ValidateUpdatedFrom(
        this Section updatedSection, 
        Section section, 
        UpdateSectionCommand command)
    {
        updatedSection.Id.Should().Be(section.Id);
        updatedSection.Name.Should().Be(command.Name);
        updatedSection.OrderIndex.Should().Be(command.OrderIndex);
        updatedSection.ProjectId.Should().Be(section.ProjectId);
        updatedSection.Tasks.Should().BeEquivalentTo(section.Tasks);
        updatedSection.CreatedAtUtc.Should().Be(section.CreatedAtUtc);
    }
}