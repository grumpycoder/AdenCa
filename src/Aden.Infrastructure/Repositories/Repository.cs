using System.Linq.Expressions;
using Aden.Application.Interfaces;
using Aden.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Aden.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T: class
{
    public ApplicationDbContext Context { get; }
    private DbSet<T> DbSet { get; set; }
    public Repository(ApplicationDbContext context)
    {
        Context = context;
        DbSet = Context.Set<T>(); 
    }
    
    public async Task<IEnumerable<T>> All()
    {
        return await DbSet.ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        DbSet.AddRange(entities);    
    }
    
    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }

    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.Where(predicate).ToListAsync();
    }
}