using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Core.Data.MongoDb
{
    public interface IRepository<TEntity, in TKey> where TEntity : IEntityBase<TKey>
    {
        Task<TEntity> GetByIdAsync<TEntity>(TKey id);

        Task<ICollection<TEntity>> GetItemsAsync<TEntity>(FilterDefinition<TEntity> where, params string[] columns);

        Task<TEntity> GetAsync<TEntity>(FilterDefinition<TEntity> filter, params string[] columns);

        Task<TEntity> DeleteAsync<TEntity>(FilterDefinition<TEntity> filter);
        
        Task<DeleteResult> DeleteAsync<TEntity>(string id);

        Task<TEntity> InsertAsync<TEntity>(TEntity entity);

        Task<TEntity> UpdateAsync<TEntity>(string id, TEntity entity);
    }
}
