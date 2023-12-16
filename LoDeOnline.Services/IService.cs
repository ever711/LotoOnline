using LoDeOnline.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LoDeOnline.Services
{
    public interface IService<T> where T : Entity
    {
        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

          /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        T Insert(T entity);

        /// <summary>
        /// Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        IEnumerable<T> Insert(IEnumerable<T> entities);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Update(T entity);

        void Update(IEnumerable<T> entities);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        void Delete(T entity);

        /// <summary>
        /// Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        void Delete(IEnumerable<T> entities);

        IQueryable<T> Search(Expression<Func<T, bool>> domain = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int offSet = 0,
            int limit = int.MaxValue);

        IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters)
          where TEntity : Entity, new();

        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);

        int SaveChanges();
    }
}
