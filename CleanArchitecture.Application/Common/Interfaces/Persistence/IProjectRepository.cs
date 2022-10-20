using CleanArchitecture.Domain.ProjectAggregates;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence;
public interface IProjectRepository {
    public void SaveChanges();
    public void Add( Project project );
    public Project? GetById( Guid id );
    public IEnumerable<Project> GetAll();
    public void Update( Project project );
}
