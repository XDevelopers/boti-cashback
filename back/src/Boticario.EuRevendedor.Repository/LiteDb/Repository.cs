using Boticario.EuRevendedor.Core.Data.LiteDb;
using Boticario.EuRevendedor.Data.LiteDb;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Boticario.EuRevendedor.Repository.LiteDb
{
    public class Repository<T> : IRepository<T> where T : IEntityBase
    {
        private readonly ILiteCollection<T> collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentException">LiteDb context has not been initialized.</exception>
        public Repository(ILiteDbContext liteDbContext)
        {
            liteDbContext = liteDbContext ??
                throw new ArgumentException("LiteDb context has not been initialized.");

            collection = liteDbContext.Database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        /// <summary>
        /// Adds the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public T Add(T model)
        {
            model.CreatedTime = DateTimeOffset.UtcNow;
            model.Id = ObjectId.NewObjectId().ToString();
            collection.Insert(model);
            
            return model;
        }

        /// <summary>
        /// Exists the specified lambda.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        public bool Exists(Expression<Func<T, bool>> lambda)
        {
            return collection.Exists(lambda);
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> FindAll()
        {
            return collection.FindAll();
        }

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> lambda)
        {
            return collection.FindOne(lambda);
        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public bool Update(T model)
        {
            return collection.Update(model);
        }

        /// <summary>
        /// Updates the specified list of model.
        /// </summary>
        /// <param name="model">The list of model.</param>
        /// <returns></returns>
        public int Update(IEnumerable<T> model)
        {
            return collection.Update(model);
        }

        /// <summary>
        /// Deletes the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public int Delete(T model)
        {
            return collection.DeleteMany(x => x.Id == model.Id);
        }

        /// <summary>
        /// Deletes the specified lambda.
        /// </summary>
        /// <param name="lambda">The lambda.</param>
        /// <returns></returns>
        public int Delete(Expression<Func<T, bool>> lambda)
        {
            return collection.DeleteMany(lambda);
        }

        private string GetCollectionName(Type type) 
        {
            var typeName = type.Name.Substring(1, type.Name.Length - 1);
            return typeName.ToLower();
        }
    }
}
