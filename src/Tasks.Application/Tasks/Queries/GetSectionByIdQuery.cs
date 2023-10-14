using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetSectionByIdQuery(Guid SectionId) : IRequest<Section?>;

public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Section?>
{
    private readonly ISectionRepository _sectionRepository;

    public GetSectionByIdQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<Section?> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        return await _sectionRepository.GetByIdAsync(request.SectionId, cancellationToken);
    }
}