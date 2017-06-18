using BLL.Interfacies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Services
{
    /// <summary>
    /// Provides methods for tag entities handling
    /// </summary>
    public interface ITagService
    {
        /// <summary>
        /// Creates new tag entity
        /// </summary>
        /// <param name="entity">Base entity for new tag</param>
        void Create(TagEntity entity);

        /// <summary>
        /// Updates tag entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        void Update(TagEntity entity);

        /// <summary>
        /// Deletes tag entity
        /// </summary>
        /// <param name="entity">Base entity for removing</param>
        void Delete(TagEntity entity);

        /// <summary>
        /// Returns tag entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Tag that satisfies predicate</returns>
        TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> f);

        /// <summary>
        /// Returns all tag entities
        /// </summary>
        /// <returns>List of tags</returns>
        IEnumerable<TagEntity> GetAll();

        /// <summary>
        /// Returns tag entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of tag entities</returns>
        IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> f);

        /// <summary>
        /// Returns tag entities that are contained in the article with specified id
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>Tag entities that are contained in the article with specified id</returns>
        IEnumerable<TagEntity> GetTagsByArticleId(int articleId);
    }
}
