using CleanArchitecture.Contracts.Projects;
using CleanArchitecture.Functional.Tests.Common;
using CleanArchitecture.WebApi;
using System.Net;
using Xunit;

namespace CleanArchitecture.Functional.Tests.Projects;
public class Create {
    [Fact( DisplayName = "Create new project, must return Ok. Check result.")]
    public async Task _NewProject_MustReturnOk() {
        var server = MockServerFactory.GetFakeServer();
        var request = new CreateProjectRequest( "new project" );

        var createProjectApiResult = await ApiRequester
            .PostAsync<ProjectDto>(
                server, $"{Route.Project}", request );

        Assert.Equal( 
            HttpStatusCode.OK, 
            createProjectApiResult.Response.StatusCode );

        Assert.Equal(
            request.Name,
            createProjectApiResult.Value.Name );
    }
}
