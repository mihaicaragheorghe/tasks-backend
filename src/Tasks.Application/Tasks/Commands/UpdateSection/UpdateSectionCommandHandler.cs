using MediatR;

using Tasks.Application.Common.Errors;

using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Section>
{
    private readonly ISectionRepository _sectionRepository;

    public UpdateSectionCommandHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<Section> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Section.NotFound);

        section.Update(request.Name, request.OrderIndex);

        return await _sectionRepository.UpdateAsync(section, cancellationToken) > 0
            ? section
            : throw new ServiceException(Errors.Section.FailedToUpdate);
    }
}