using MediatR;

using Application.Common.ErrorHandling;
using Application.Core;
using Domain;

namespace Application.Sections.Commands;

public record DeleteSectionCommand(Guid Id) : IRequest<Unit>;

public class DeleteSectionCommandHandler(ISectionRepository sectionRepository) 
    : IRequestHandler<DeleteSectionCommand, Unit>
{
    private readonly ISectionRepository _sectionRepository = sectionRepository;

    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Section.NotFound);

        return await _sectionRepository.DeleteAsync(section, cancellationToken) > 0
            ? Unit.Value
            : throw new ServiceException(Errors.Section.DeleteFailed);
    }
}