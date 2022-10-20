using CleanArchitecture.Domain.ProjectAggregates;

namespace CleanArchitecture.Application.Projects.Common;
public record ProjectsResult( IEnumerable<Project> Projects );
