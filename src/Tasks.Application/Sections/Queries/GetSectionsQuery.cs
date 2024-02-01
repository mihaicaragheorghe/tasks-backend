using MediatR;
using Domain;

namespace Application.Tasks.Queries;

public record GetSectionsQuery(Guid ProjectId) : IRequest<List<Section>>;

public class GetSectionsQueryHandler(ISectionRepository sectionRepository) 
    : IRequestHandler<GetSectionsQuery, List<Section>>
{
    public async Task<List<Section>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
    {
        return await sectionRepository.GetAllAsync(request.ProjectId, cancellationToken);
    }
}