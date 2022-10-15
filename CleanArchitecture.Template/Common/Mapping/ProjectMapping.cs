using CleanArchitecture.Application.Projects.Common;
using CleanArchitecture.Contracts.Projects;
using CleanArchitecture.Domain.Aggregates;
using Mapster;

namespace CleanArchitecture.WebApi.Common.Mapping;

public class ProjectMapping : IRegister {
    public void Register( TypeAdapterConfig config ) {
        config.NewConfig<ProjectResult, ProjectDto>()
            .Map( dest => dest, src => src.Project );

        config.NewConfig<IEnumerable<Project>, ProjectsDto>()
            .Map( dest => dest.Projects, src => src );
    }
}
