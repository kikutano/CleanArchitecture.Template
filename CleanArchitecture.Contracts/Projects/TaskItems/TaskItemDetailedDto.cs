using CleanArchitecture.Domain.ProjectAggregates;

namespace CleanArchitecture.Contracts.Projects.TaskItems;
public record TaskItemDetailedDto( 
    Guid Id, 
    string Title, 
    string? Description, 
    TaskState State );
