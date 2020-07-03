using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Core.Data.MongoDb
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntityBase<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);

        Task<ICollection<TEntity>> GetItemsAsync(FilterDefinition<TEntity> where, params string[] columns);

        Task<TEntity> GetAsync(FilterDefinition<TEntity> filter, params string[] columns);

        Task<TEntity> DeleteAsync(FilterDefinition<TEntity> filter);
        
        Task<DeleteResult> DeleteAsync(string id);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<TEntity> UpdateAsync(string id, TEntity entity);
    }
}
