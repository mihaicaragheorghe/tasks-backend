using MediatR;

using Tasks.Application.Common.Errors;

using Tasks.Application.Common.Repository;
using Tasks.Application.Core;

namespace Tasks.Application.Tasks.Commands;

public record DeleteSectionCommand(Guid Id) : IRequest<Unit>;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Unit>
{
    private readonly ISectionRepository _sectionRepository;

    public DeleteSectionCommandHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<Unit> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        var section = await _sectionRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Section.NotFound);

        return await _sectionRepository.DeleteAsync(section, cancellationToken) > 0
            ? Unit.Value
            : throw new ServiceException(Errors.Section.DeleteFailed);
    }
}