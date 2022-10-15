using CleanArchitecture.Application.Projects.Common;
using ErrorOr;
using MediatR;

namespace CleanArchitecture.Application.Projects.Commands;
public record CreateProjectCommand( string Name ) : IRequest<ErrorOr<ProjectResult>>;
