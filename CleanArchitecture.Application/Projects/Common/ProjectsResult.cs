using CleanArchitecture.Domain.Aggregates;

namespace CleanArchitecture.Application.Projects.Common;
public record ProjectsResult( IEnumerable<Project> Projects );
