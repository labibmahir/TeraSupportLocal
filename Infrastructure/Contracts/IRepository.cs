using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Contracts;

public interface IRepository<T>
{
    T Add(T entity);
    
    T Get(Guid id);
    
    T Get(int id);
    
    IEnumerable<T> GetAll();
    
    void AddRange(IEnumerable<T> entities);
    
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj);
    
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>> obj, Expression<Func<T, object>> next);
    
    Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);
    
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    T GetById(Guid id);
    
    T GetById(int key);
    
    Task<T?> GetByIdAsync(Guid id);
    
    Task<T?> GetByIdAsync(int key);
    
    void Update(T entity);
    
    void Delete(T entity);
    
    Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] expressionList);
    
    Task<IEnumerable<T>> LoadListWithChildAsync<TEntity>(Expression<Func<T, bool>> predicate, int skip, int take, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] expressionList);
    
    int Count(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] expressionList);
    
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

}