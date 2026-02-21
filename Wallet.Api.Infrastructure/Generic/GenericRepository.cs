using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Wallet.Api.Domain.WalletDbModel;

namespace Wallet.Api.Infrastructure.Generic
{

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly WalletDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(WalletDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public virtual async Task<int> AddAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return 1;

        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        public IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null)
        {
            if (predicate != null)
            {
                return _dbSet.Where(predicate);
            }

            return _dbSet.AsQueryable();
        }
        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
