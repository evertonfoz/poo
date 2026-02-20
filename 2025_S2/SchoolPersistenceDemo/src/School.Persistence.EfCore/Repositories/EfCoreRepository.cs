using Microsoft.EntityFrameworkCore;
using School.Domain.Repositories;
using School.Persistence.EfCore.Context;

namespace School.Persistence.EfCore.Repositories;

public class EfCoreRepository<T, TId> : IReadRepository<T, TId>,
   IWriteRepository<T, TId> where T : class
{
    protected readonly SchoolDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public EfCoreRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new
           ArgumentNullException(nameof(dbContext));
        _dbSet = _dbContext.Set<T>();
    }
    public T? GetById(TId id)
    {
        return _dbSet.Find(id);
    }

    public IReadOnlyList<T> ListAll()
    {
        return [.. _dbSet.AsNoTracking()];
    }

    public IReadOnlyList<T> Find(Func<T, bool> predicate)
    {
        ArgumentNullException.ThrowIfNull(predicate);
        // Atenção: aqui o filtro é aplicado em memória após o ToList()
        return [.. _dbSet.AsNoTracking().Where(predicate)];
    }

    public T Add(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _dbSet.Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public void Update(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        _dbSet.Update(entity);
        _dbContext.SaveChanges();
    }

    public void Remove(TId id)
    {
        var entity = GetById(id);
        if (entity is null) return;
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }
}