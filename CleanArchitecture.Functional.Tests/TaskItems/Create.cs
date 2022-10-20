using CleanArchitecture.Common.Tests.DatabaseSnapshot;
using CleanArchitecture.Contracts.Projects.TaskItems;
using CleanArchitecture.Domain.ProjectAggregates;
using CleanArchitecture.Functional.Tests.Common;
using CleanArchitecture.WebApi;
using System.Net;
using Xunit;

namespace CleanArchitecture.Functional.Tests.TaskItems;
public class Create {
    [Fact( DisplayName = "Create new task item inside a project, must return Ok. Check result." )]
    public async Task _NewProject_MustReturnOk() {
        var server = MockServerFactory.GetFakeServer();

        Project project;

        MockServerFactory
            .DbSnapshoter
            .AddProject( out project );

        var request = new CreateTaskRequest( 
            "Amazing Task!", 
            "This is an amazing task!" );

        var createProjectApiResult = await ApiRequester
            .PostAsync<TaskItemDetailedDto>(
                server, $"{Route.TaskItem}/{project.Id}", request );

        Assert.Equal(
            HttpStatusCode.OK,
            createProjectApiResult.Response.StatusCode );
        Assert.Equal(
            request.Title,
            createProjectApiResult.Value.Title );
        Assert.Equal(
           request.Decription,
           createProjectApiResult.Value.Description );
    }
}