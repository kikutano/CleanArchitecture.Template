using CleanArchitecture.Application.TaskItems.Common;
using ErrorOr;
using MediatR;

namespace CleanArchitecture.Application.TaskItems.Commands;
public record CreateTaskItemCommand( 
    Guid ProjectId, 
    string Title, 
    string? Description ) 
    : IRequest<ErrorOr<TaskItemResult>>;
