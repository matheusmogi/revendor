using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revendor.Domain.Interfaces
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T : class; 
        Task AddAsync<T>(T entity) where T : class;
        Task AddRangeAsync<T>(IList<T> entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<int> SaveChangesAsync();
        Task Remove<T>(T entity)where T : class;
        IQueryable<T> QueryFromStoredProcedure<T>(string procName) where T : class; 
    }
}