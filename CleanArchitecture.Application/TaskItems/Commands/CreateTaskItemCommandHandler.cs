using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.TaskItems.Common;
using CleanArchitecture.Domain.ProjectAggregates.Entities;
using CleanArchitecture.Domain.ValueObjects;
using ErrorOr;
using MediatR;
using Errors = CleanArchitecture.Domain.ProjectAggregates.Errors.Errors;

namespace CleanArchitecture.Application.TaskItems.Commands;
internal sealed class CreateTaskItemCommandHandler
    : IRequestHandler<CreateTaskItemCommand, ErrorOr<TaskItemResult>> {
    private readonly IProjectRepository _projectRepository;

    public CreateTaskItemCommandHandler( IProjectRepository projectRepository ) {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<TaskItemResult>> Handle(
        CreateTaskItemCommand request, CancellationToken cancellationToken ) {

        var project = _projectRepository.GetById( request.ProjectId );

        if ( project is null )
            return Errors.Project.NotFound( request.ProjectId );

        var title = LimitedText.Create( request.Title );

        if ( title.IsError )
            return title.Errors;

        DescriptionText? description = null;
        if ( !string.IsNullOrEmpty( request.Description ) ) {
            var errorOrDescription = DescriptionText.Create( request.Description );

            if ( errorOrDescription.IsError )
                return errorOrDescription.Errors;

            description = errorOrDescription.Value;
        }

        var taskItem = TaskItem.Create( project.Id, title.Value, description );

        project.AddTask( taskItem.Value );
        _projectRepository.Update( project );
        return await Task.FromResult( new TaskItemResult( taskItem.Value ) );
    }
}

