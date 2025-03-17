using Domain.Abstractions.IBaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly InMemoryDB.InMemoryDB _context;
        private readonly DbSet<TEntity> _set;

        protected BaseRepository(InMemoryDB.InMemoryDB context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }

        public Task<IQueryable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>>? expression = default,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> set = _set;

            if (expression is not null)
            {
                set = set.Where(expression);
            }

            if (orderBy is not null)
            {
                set = orderBy(set);
            }

            return Task.FromResult(set);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression) => _set.Where(expression);
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public ValueTask AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.AddAsync(entity, cancellationToken);
            return ValueTask.CompletedTask;
        }
        public ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            _set.Update(entity);
            return ValueTask.CompletedTask;
        }
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default) => _set.AnyAsync(expression, cancellationToken);
        public void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }
    }
}
