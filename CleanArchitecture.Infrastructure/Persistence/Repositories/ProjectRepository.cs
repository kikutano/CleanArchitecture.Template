using CleanArchitecture.Application.Common.Interfaces.Persistence;
using CleanArchitecture.Domain.ProjectAggregates;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence.Repositories;
public class ProjectRepository : IProjectRepository {
    private readonly CleanArchitectureDbContext _context;

    public ProjectRepository( CleanArchitectureDbContext context ) {
        _context = context;
    }

    public void Add( Project project ) {
        _context.Projects.Add( project );
        SaveChanges();
    }

    public IEnumerable<Project> GetAll() {
        return _context
            .Projects
            .AsNoTracking()
            .ToList();
    }

    public Project? GetById( Guid id ) {
        var project = _context
            .Projects
            .Include( project => project.TaskItems )
            .FirstOrDefault( project => project.Id == id );

        return project;
    }

    public void Update( Project project ) {
        try {
            _context.Projects.Update( project );
            SaveChanges();
        }
        catch ( Exception e ) {
            int a = 0;
        }
       
    }

    public void SaveChanges() {
        _context.SaveChanges();
    }
}
