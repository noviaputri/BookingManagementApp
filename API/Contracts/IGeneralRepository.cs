namespace API.Contracts;

// Declares a new public interface named IGeneralRepository with a generic type parameter TEntity.
public interface IGeneralRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> GetAll(); // Declares a method named GetAll that returns an IEnumerable of TEntity objects.
    TEntity? GetByGuid(Guid guid); // Declares a method named GetByGuid that takes a Guid parameter and returns a TEntity object.
    TEntity? Create(TEntity entity); // Declares a method named Create that takes a TEntity parameter and returns a TEntity object.
    bool Update(TEntity entity); // Declares a method named Update that takes a TEntity parameter and returns a boolean value.
    bool Delete(TEntity entity); // Declares a method named Delete that takes a TEntity parameter and returns a boolean value.
}
