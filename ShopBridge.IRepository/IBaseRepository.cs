using Microsoft.EntityFrameworkCore.Storage;
using ShopBridge.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(int pageNumber, int pageSize);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(int id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Add(TEntity entity);
        Task AddRange(List<TEntity> entities);
        Task<TEntity> Update(TEntity entity);
        Task Delete(int Id);
        IDbContextTransaction BeginTransaction();
    }
}
