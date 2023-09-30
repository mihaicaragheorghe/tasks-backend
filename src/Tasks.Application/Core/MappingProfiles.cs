using AutoMapper;
using Tasks.Application.Tasks;

namespace Tasks.Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateProjectRequest, CreateProjectCommand>();
    }
}