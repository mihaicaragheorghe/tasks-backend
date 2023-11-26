using MediatR;
using Tasks.Application.Common.Errors;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.TaskSections.Commands;

public class CreateSectionCommandHandler(ISectionRepository taskSectionRepository) 
    : IRequestHandler<CreateSectionCommand, Section>
{
    private readonly ISectionRepository _taskSectionRepository = taskSectionRepository;

    public async Task<Section> Handle(CreateSectionCommand command, CancellationToken cancellationToken)
    {
        var section = Section.Create(command.ProjectId, command.Name);

        bool success = await _taskSectionRepository.AddAsync(section, cancellationToken) > 0;

        return !success ? throw new ServiceException(Errors.Section.FailedToCreate) : section;
    }
}