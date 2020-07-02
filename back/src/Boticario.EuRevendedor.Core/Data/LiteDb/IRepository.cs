using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Boticario.EuRevendedor.Core.Data.LiteDb
{
    /// <summary>
    /// Interface to share method most used between repository classes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        T Add(T model);

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int Delete(T model);

        /// <summary>
        /// Deletes the specified lambda.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        int Delete(Expression<Func<T, bool>> lambda);

        /// <summary>
        /// Exists the specified lambda.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        bool Exists(Expression<Func<T, bool>> lambda);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> lambda);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        bool Update(T model);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        int Update(IEnumerable<T> model);
    }
}
