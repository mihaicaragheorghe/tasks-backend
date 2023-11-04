using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetSectionsQuery(Guid ProjectId) : IRequest<List<Section>>;

public class GetSectionsQueryHandler : IRequestHandler<GetSectionsQuery, List<Section>>
{
    private readonly ISectionRepository _sectionRepository;

    public GetSectionsQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<List<Section>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
    {
        return await _sectionRepository.GetAllAsync(request.ProjectId, cancellationToken);
    }
}