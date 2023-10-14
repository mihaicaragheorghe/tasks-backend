using MediatR;
using Tasks.Application.Common.Errors;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Section>
{
    private readonly ISectionRepository _taskSectionRepository;

    public CreateSectionCommandHandler(ISectionRepository taskSectionRepository)
    {
        _taskSectionRepository = taskSectionRepository;
    }

    public async Task<Section> Handle(CreateSectionCommand command, CancellationToken cancellationToken)
    {
        var section = Section.Create(command.ProjectId, command.Name);

        bool success = await _taskSectionRepository.AddAsync(section, cancellationToken) > 0;

        if (!success)
        {
            throw new ServiceException(Errors.Section.FailedToCreate);
        }

        return section;
    }
}