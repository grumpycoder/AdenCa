using System.Linq.Expressions;

namespace Aden.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> All();
    Task<T> GetById(int id);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
}