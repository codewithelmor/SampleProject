using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleProject.DataAccessLayer.UnitOfWorks;
using SampleProject.DomainObject;

namespace SampleProject.DataAccessLayer.Repositories
{
    public class BaseRepository<TEntity, TDbContext> : IBaseRepository<TEntity, TDbContext> 
        where TEntity : BaseEntity 
        where TDbContext : DbContext
    {
        protected readonly DbSet<TEntity> Entities;

        public BaseRepository(IUnitOfWork<TDbContext> unitOfWork)
        {
            Entities = unitOfWork.GetDbContext().Set<TEntity>();
        }

        public async Task<TEntity> GetAsync(dynamic id)
        {
            return await Entities.FindAsync(id);
        }

        public TEntity Get(dynamic id)
        {
            return Entities.Find(id);
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return Entities;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.SingleOrDefault(predicate);
        }

        public TEntity Add(TEntity entity)
        {
            var result = Entities.Add(entity);
            return result.Entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result =  await Entities.AddAsync(entity);
            return result.Entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public TEntity Update(TEntity entity)
        {
            Entities.Update(entity);
            return entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            var removeRange = entities as TEntity[] ?? entities.ToArray();
            Entities.UpdateRange(removeRange);
            return removeRange;
        }

        public TEntity Remove(TEntity entity)
        {
            Entities.Remove(entity);
            return entity;
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            var removeRange = entities as TEntity[] ?? entities.ToArray();
            Entities.RemoveRange(removeRange);
            return removeRange;
        }
    }
}