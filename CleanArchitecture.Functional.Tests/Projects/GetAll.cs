using CleanArchitecture.Common.Tests.DatabaseSnapshot;
using CleanArchitecture.Contracts.Projects;
using CleanArchitecture.Domain.ProjectAggregates;
using CleanArchitecture.Functional.Tests.Common;
using CleanArchitecture.WebApi;
using Xunit;

namespace CleanArchitecture.Functional.Tests.Projects;
public class GetAll {
    [Fact( DisplayName = "Get all projects, must return 3 projects." )]
    public async Task _GetProjectById_MustReturnOk() {
        var server = MockServerFactory.GetFakeServer();

        Project project;

        MockServerFactory
            .DbSnapshoter
            .AddProject( out project, "new project 1" )
            .AddProject( out project, "new project 2" )
            .AddProject( out project, "new project 3" );

        var getProjectApiResult = await ApiRequester
            .GetAsync<ProjectsDto>(
                server, $"{Route.Project}" );

        Assert.Equal( 3, getProjectApiResult.Value.Projects.Count() );
    }
}