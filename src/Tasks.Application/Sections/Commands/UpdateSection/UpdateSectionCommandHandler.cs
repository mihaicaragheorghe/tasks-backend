using MediatR;

using Application.Common.ErrorHandling;

using Application.Core;
using Domain;

namespace Application.Sections.Commands;

public class UpdateSectionCommandHandler(ISectionRepository sectionRepository) 
    : IRequestHandler<UpdateSectionCommand, Section>
{
    private readonly ISectionRepository _sectionRepository = sectionRepository;

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