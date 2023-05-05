using CompetitionEventsManager.Models;
using System.Linq.Expressions;

namespace CompetitionEventsManager.Repository.IRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // CRUD    
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, bool tracked = true);
        Task CreateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
        Task RemoveAsync(TEntity entity);
        Task SaveAsync();
        Task UpdateAsync(TEntity entity);
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> filter);
        Task<TEntity> GetFewDBAsync(Expression<Func<TEntity, bool>> filter, ICollection<string> includeTables, bool tracked = true);
        Task<List<TEntity>> GetAllFewDBAsync(Expression<Func<TEntity, bool>>? filter, ICollection<string> includeTables);
      
    }
}