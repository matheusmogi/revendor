using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Revendor.Domain.Interfaces;

namespace Revendor.Infrastructure.Persistence
{
    public class Repository : IRepository
    {
        private readonly RevendorContext dbContext;

        public Repository(RevendorContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return dbContext.Set<T>();
        }

        public Task AddAsync<T>(T entity) where T : class
        {
            return dbContext.AddAsync(entity).AsTask();
        }

        public Task AddRangeAsync<T>(IList<T> entity) where T : class
        {
            return dbContext.AddRangeAsync(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            dbContext.Update(entity);
        }

        public Task Remove<T>(T entity) where T : class
        {
            return Task.Run(() => dbContext.Remove(entity));
        }

        public IQueryable<T> QueryFromStoredProcedure<T>(string procName) where T : class
        {
            return null; //dbContext.Set<T>().FromSqlRaw($"execute dbo.{procName}");
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}