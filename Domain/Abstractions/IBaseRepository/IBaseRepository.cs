using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.IBaseRepository
{
    public interface IBaseRepository<TEntity>
    {
        Task<IQueryable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>>? expression = default,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default,
          CancellationToken cancellationToken = default);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression,CancellationToken cancellationToken=default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Remove(TEntity entity);

    }
}
