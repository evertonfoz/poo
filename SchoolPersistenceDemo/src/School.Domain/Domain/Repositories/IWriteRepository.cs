namespace School.Domain.Repositories;

public interface IWriteRepository<T, TId>
{
    T Add(T entity);
    void Update(T entity);
    void Remove(TId id);
}