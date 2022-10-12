namespace CleanArchitecture.Domain.Common.Models;
public abstract class BaseEntity {
    public Guid Id { get; }

    public BaseEntity( Guid id ) {
        Id = id;
    }
}
