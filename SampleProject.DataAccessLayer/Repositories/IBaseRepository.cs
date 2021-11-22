using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleProject.DataAccessLayer.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        bool IsExisting(Expression<Func<TEntity, bool>> predicate);
        Task<bool> IsExistingAsync(Expression<Func<TEntity, bool>> predicate);
        bool IsEmpty();
        Task<TEntity> GetAsync(dynamic id);
        TEntity Get(dynamic id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
        TEntity Remove(TEntity entity);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities);
        IEnumerable<TEntity> RemoveAll();
    }
}