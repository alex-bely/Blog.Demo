using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    /// <summary>
    /// Provides base mathods entities handling
    /// </summary>
    /// <typeparam name="TEntity">Entyty</typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Creates new entity
        /// </summary>
        /// <param name="e">Base entity for new entity</param>
        void Create(TEntity e);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="e">Base entity for removing</param>
        void Delete(TEntity e);

        /// <summary>
        /// Updates entity 
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        void Update(TEntity entity);

        /// <summary>
        /// Returns all entities
        /// </summary>
        /// <returns>Sequence of entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Returns entity with specified Id key
        /// </summary>
        /// <param name="key">Entity Id</param>
        /// <returns>Entity with specified Id</returns>
        TEntity GetById(int key);

        /// <summary>
        /// Returns entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of entities</returns>
        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> f);

        /// <summary>
        /// Returns entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Entity that satisfies predicate</returns
        TEntity GetOneByPredicate(Expression<Func<TEntity, bool>> f);
        
    }
}
