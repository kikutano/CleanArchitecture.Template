using CleanArchitecture.Domain.ProjectAggregates;

namespace CleanArchitecture.Contracts.Projects.TaskItems;
public record TaskItemDto( Guid Id, string Title, TaskState State );
