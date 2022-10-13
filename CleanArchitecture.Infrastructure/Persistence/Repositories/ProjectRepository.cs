using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Domain.Aggregates;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;
public class ProjectRepository : IProjectRepository {
    private readonly CleanArchitectureDbContext _context;

    public ProjectRepository( CleanArchitectureDbContext context ) {
        _context = context;
    }

    public void Add( Project project ) {
        _context.Projects.Add( project );
        _context.SaveChanges();
    }

    public IEnumerable<Project> GetAll() {
        throw new NotImplementedException();
    }

    public Project GetById( Guid id ) {
        throw new NotImplementedException();
    }
}
