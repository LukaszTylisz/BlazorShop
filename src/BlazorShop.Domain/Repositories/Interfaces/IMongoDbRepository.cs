using System.Linq.Expressions;

namespace BlazorShop.Domain.Repositories.Interfaces;

public interface IMongoDbRepository
{
    Task<IEnumerable<T>> FindAll<T>(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetAll<T>();
    Task<T> GetById<T>(Guid id);
    Task<bool> Any<T>();
    Task Insert<T>(T entity);
    Task Update<T>(Guid id, T entity);
    Task Delete<T>(Guid id);
}