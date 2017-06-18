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
    /// Provides methods for comment entities handling
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Creates new comment entity
        /// </summary>
        /// <param name="commentEntity">ase entity for new comment entity</param>
        void Create(CommentEntity commentEntity);

        /// <summary>
        /// Deletes comment entity
        /// </summary>
        /// <param name="commentEntity">Base entity for removing</param>
        void Delete(CommentEntity commentEntity);

        /// <summary>
        /// Updates comment entity
        /// </summary>
        /// <param name="commentEntity">Base entity for updating</param>
        void Update(CommentEntity commentEntity);

        /// <summary>
        /// Returns comment entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Comment that satisfies predicate</returns>
        CommentEntity GetOneByPredicate(Expression<Func<CommentEntity, bool>> f);

        /// <summary>
        /// Returns all comment entities
        /// </summary>
        /// <returns>Sequence of comments</returns
        IEnumerable<CommentEntity> GetAllComments();

        /// <summary>
        /// Returns comment entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of comment entities</returns
        IEnumerable<CommentEntity> GetAllByPredicate(Expression<Func<CommentEntity, bool>> f);
    }
}
