using MediatR;
using Domain;

namespace Application.Tasks.Queries;

public record GetSectionByIdQuery(Guid SectionId) : IRequest<Section?>;

public class GetSectionByIdQueryHandler(ISectionRepository sectionRepository) 
    : IRequestHandler<GetSectionByIdQuery, Section?>
{
    public async Task<Section?> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
    {
        return await sectionRepository.GetByIdAsync(request.SectionId, cancellationToken);
    }
}