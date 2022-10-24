using CleanArchitecture.Common.Tests.DatabaseSnapshot;
using CleanArchitecture.Contracts.Projects.TaskItems;
using CleanArchitecture.Domain.ProjectAggregates;
using CleanArchitecture.Functional.Tests.Common;
using CleanArchitecture.WebApi;
using Xunit;

namespace CleanArchitecture.Functional.Tests.TaskItems;
public class GetAll {
    [Fact( DisplayName = "Get all task items inside a project, must return 2" )]
    public async Task _NewProject_MustReturnOk() {
        var server = MockServerFactory.GetFakeServer();

        Project project;

        MockServerFactory
            .DbSnapshoter
            .AddProject( out project )
            .AddTaskItem( project.Id )
            .AddTaskItem( project.Id );

        var getAllTaskItemsResult = await ApiRequester
            .GetAsync<TaskItemsDto>(
                server, $"{Route.TaskItem}/{project.Id}" );

        Assert.Equal(
            2,
            getAllTaskItemsResult.Value.TaskItems.Count() );
    }
}
