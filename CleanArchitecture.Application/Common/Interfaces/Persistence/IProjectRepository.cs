using CleanArchitecture.Domain.Aggregates;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence;
public interface IProjectRepository {
    public void Add( Project project );
    public Project GetById( Guid id );
    public IEnumerable<Project> GetAll();
}
