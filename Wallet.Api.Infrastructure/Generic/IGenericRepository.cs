using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wallet.Api.Infrastructure.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        IQueryable<T> Get(Expression<Func<T, bool>>? predicate = null);
        Task<bool> DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();

    }
}
