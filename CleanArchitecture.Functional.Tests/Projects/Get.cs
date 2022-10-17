using CleanArchitecture.Common.Tests.DatabaseSnapshot;
using CleanArchitecture.Contracts.Projects;
using CleanArchitecture.Domain.Aggregates;
using CleanArchitecture.Functional.Tests.Common;
using CleanArchitecture.WebApi;
using System.Net;
using Xunit;

namespace CleanArchitecture.Functional.Tests.Projects;
public class Get {
    [Fact( DisplayName = "Get a project by Id, must return ok." )]
    public async Task _GetProjectById_MustReturnOk() {
        var server = MockServerFactory.GetFakeServer();

        Project project;

        MockServerFactory
            .DbSnapshoter
            .AddProject( out project, "new project 1" );

        var getProjectApiResult = await ApiRequester
            .GetAsync<ProjectDto>(
                server, $"{Route.Project}/{project.Id}" );

        Assert.Equal(
            HttpStatusCode.OK,
            getProjectApiResult.Response.StatusCode );

        Assert.Equal(
            project.Name.Value,
            getProjectApiResult.Value.Name );
    }
}
