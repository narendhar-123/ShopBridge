using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ShopBridge.Domain.Model;
using ShopBridge.IRepository;
using ShopBridge.Repository.Context;
using ShopBridge.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ShopBridgeContext _dbContext;
        internal DbSet<TEntity> _dbSet;

        public BaseRepository(ShopBridgeContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public virtual IQueryable<TEntity> GetAll(int pageNumber, int pageSize)
        {
            return PaginatorUtility<TEntity>.ApplyPaging(GetAll(), pageNumber, pageSize);
        }
        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().Where(predicate);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            _dbSet.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task AddRange(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            await SaveChangesAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await SaveChangesAsync();
            return entity;
        }

        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return _dbContext.Database.BeginTransaction();
        }

        public virtual async Task Delete(int Id)
        {
            TEntity entityToDelete = _dbSet.Find(Id);
            await Delete(entityToDelete);
        }

        public virtual async Task Delete(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbContext.Attach(entityToDelete);
            }
            _dbContext.Remove(entityToDelete);
            await SaveChangesAsync();
        }
    }
}
