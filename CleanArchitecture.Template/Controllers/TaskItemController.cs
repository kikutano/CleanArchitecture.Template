using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.TaskItems.Commands;
using CleanArchitecture.Contracts.Projects;
using CleanArchitecture.Contracts.Projects.TaskItems;
using CleanArchitecture.Domain.ProjectAggregates.Errors;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CleanArchitecture.WebApi.Controllers;

[Route( Route.TaskItem )]
public class TaskItemController : ApiController {
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public TaskItemController(
        ISender mediator,
        IMapper mapper,
        IProjectRepository projectRepository ) {
        _mediator = mediator;
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Create a new task inside a Project
    /// </summary>
    /// <returns>TaskItem informations</returns>
    [HttpPost]
    [Route( "{projectId:guid}" )]
    [SwaggerResponse( (int)HttpStatusCode.OK, Type = typeof( TaskItemDetailedDto ) )]
    [SwaggerResponse( (int)HttpStatusCode.NotFound, Type = typeof( Error ) )]
    public async Task<IActionResult> Create( Guid projectId, CreateTaskRequest request ) {
        var command = new CreateTaskItemCommand( 
            projectId, 
            request.Title, 
            request.Decription );
        var commandResult = await _mediator.Send( command );

        return commandResult.Match(
            commandResult => Ok( commandResult.Adapt<TaskItemDetailedDto>() ),
            errors => Problem( errors ) );
    }

    /// <summary>
    /// Get all task items inside a project
    /// </summary>
    /// <returns>List of Task Items</returns>
    [HttpGet]
    [Route( "{projectId:guid}" )]
    [SwaggerResponse( (int)HttpStatusCode.OK, Type = typeof( TaskItemsDto ) )]
    [SwaggerResponse( (int)HttpStatusCode.NotFound, Type = typeof( Error ) )]
    public IActionResult GetAll( Guid projectId ) {
        var project = _projectRepository.GetById( projectId );

        if ( project is null )
            return Problem( Errors.Project.NotFound( projectId ) );

        return Ok( _mapper.Map<TaskItemsDto>( project.TaskItems ) );
    }
}
