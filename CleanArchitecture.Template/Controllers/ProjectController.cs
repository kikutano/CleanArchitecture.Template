using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Application.Projects.Commands;
using CleanArchitecture.Contracts.Projects;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace CleanArchitecture.WebApi.Controllers;

[Route( Route.Project )]
public class ProjectController : ApiController {
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    
    public ProjectController( 
        ISender mediator, 
        IMapper mapper, 
        IProjectRepository projectRepository ) {
        _mediator = mediator;
        _mapper = mapper;
        _projectRepository = projectRepository;
    }

    /// <summary>
    /// Get all projects inside the system
    /// </summary>
    /// <returns>List of Projects</returns>
    [HttpGet]
    [SwaggerResponse( (int)HttpStatusCode.OK, Type = typeof( ProjectsDto ) )]
    public IActionResult GetAll() {
        var projects = _projectRepository.GetAll();

        return Ok( _mapper.Map<ProjectsDto>( projects ) );
    }

    /// <summary>
    /// Get a projects inside the system
    /// </summary>
    /// <param name="id">project idetifier</param>
    /// <returns>List of Projects</returns>
    [HttpGet]
    [Route( "{id:guid}" )]
    [SwaggerResponse( (int)HttpStatusCode.OK, Type = typeof( ProjectDto ) )]
    [SwaggerResponse( (int)HttpStatusCode.BadRequest, Type = typeof( Error ) )]
    [SwaggerResponse( (int)HttpStatusCode.NotFound, Type = typeof( Error ) )]
    public IActionResult GetById( Guid id ) {
        var project = _projectRepository.GetById( id );

        if ( project is null )
            return NotFound();

        return Ok( project.Adapt<ProjectDto>() );
    }

    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="request">body of the request</param>
    /// <returns>List of Projects</returns>
    [HttpPost]
    [SwaggerResponse( (int)HttpStatusCode.OK, Type = typeof( ProjectDto ) )]
    [SwaggerResponse( (int)HttpStatusCode.BadRequest, Type = typeof( Error ) )]
    public async Task<IActionResult> Create( CreateProjectRequest request ) {
        var command = new CreateProjectCommand( request.Name );
        var commandResult = await _mediator.Send( command );

        return commandResult.Match(
            commandResult => Ok( commandResult.Adapt<ProjectDto>() ),
            errors => Problem( errors ) );
    }
}
