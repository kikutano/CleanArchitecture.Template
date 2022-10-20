using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Projects.Common;
using CleanArchitecture.Domain.ProjectAggregates;
using CleanArchitecture.Domain.ValueObjects;
using ErrorOr;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands;
internal sealed class CreateProjectCommandHandler
    : IRequestHandler<CreateProjectCommand, ErrorOr<ProjectResult>> {
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommandHandler( IProjectRepository projectRepository ) {
        _projectRepository = projectRepository;
    }

    public async Task<ErrorOr<ProjectResult>> Handle(
        CreateProjectCommand request, CancellationToken cancellationToken ) {

        var projectName = LimitedText.Create( request.Name );

        if ( projectName.IsError )
            return projectName.Errors;

        var project = Project.Create( projectName.Value );

        if ( project.IsError )
            return project.Errors;

        _projectRepository.Add( project.Value );
        return await Task.FromResult( new ProjectResult( project.Value ) );
    }
}
