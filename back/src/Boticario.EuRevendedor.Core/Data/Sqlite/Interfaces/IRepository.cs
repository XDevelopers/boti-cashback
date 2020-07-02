using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Boticario.EuRevendedor.Core.Data.Sqlite.Interfaces
{
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        DbContext Context { get; set; }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        DbSet<T> Entities { get; }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);

        /// <summary>
        /// Adds the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(T entity);

        /// <summary>
        /// Disables the lazy load.
        /// </summary>
        void DisableLazyLoad();

        /// <summary>
        /// Enables the lazy load.
        /// </summary>
        void EnableLazyLoad();

        /// <summary>
        /// Exists the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        bool Exists(Guid id);

        /// <summary>
        /// Exists the specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Guid id);

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        List<T> FindAll(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds all asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        List<T> GetAll();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAllAsync();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(Guid id);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Gets the first.
        /// </summary>
        /// <returns></returns>
        T GetFirst();

        /// <summary>
        /// Gets the first asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<T> GetFirstAsync();

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id"></param>
        void Update(T entity, Guid id);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity, Guid id);
    }
}
