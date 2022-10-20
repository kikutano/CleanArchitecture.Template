using CleanArchitecture.Application.TaskItems.Common;
using CleanArchitecture.Contracts.Projects.TaskItems;
using Mapster;

namespace CleanArchitecture.WebApi.Common.Mapping;

public class TaskItemMapping : IRegister {
    public void Register( TypeAdapterConfig config ) {
        config.NewConfig<TaskItemResult, TaskItemDetailedDto>()
            .Map( dest => dest, src => src.TaskItem )
            .Map( dest => dest.Description, src => src.TaskItem.Description );

        //config.NewConfig<IEnumerable<Project>, ProjectsDto>()
        //    .Map( dest => dest.Projects, src => src );
    }
}
