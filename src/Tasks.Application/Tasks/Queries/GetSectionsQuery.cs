using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetSectionsQuery(Guid ProjectId) : IRequest<IEnumerable<Section>>;

public class GetSectionsQueryHandler : IRequestHandler<GetSectionsQuery, IEnumerable<Section>>
{
    private readonly ISectionRepository _sectionRepository;

    public GetSectionsQueryHandler(ISectionRepository sectionRepository)
    {
        _sectionRepository = sectionRepository;
    }

    public async Task<IEnumerable<Section>> Handle(GetSectionsQuery request, CancellationToken cancellationToken)
    {
        return await _sectionRepository.GetAllAsync(request.ProjectId, cancellationToken);
    }
}