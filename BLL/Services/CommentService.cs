using BLL.Interfacies.Services;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using System.Linq.Expressions;
using DAL.Interfacies.Repository;
using BLL.Mappers;
using ExpressionTreeVisitor;
using DAL.Interfacies.DTO;

namespace BLL.Services
{
    /// <summary>
    /// Provides methods for comment entities handling
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork uow;
        private readonly ICommentRepository commentRepository;

        /// <summary>
        /// Initializes new comment service instance
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance for the service</param>
        /// <param name="commentRepository">Comment repository instance for comments handling</param>
        public CommentService(IUnitOfWork unitOfWork, ICommentRepository commentRepository)
        {
            this.uow = unitOfWork;
            this.commentRepository = commentRepository;
        }

        /// <summary>
        /// Creates new comment entity
        /// </summary>
        /// <param name="commentEntity">Base entity for new comment entity</param>
        public void Create(CommentEntity commentEntity)
        {
            commentRepository.Create(commentEntity.ToDalComment());
            uow.Commit();
        }

        /// <summary>
        /// Deletes comment entity
        /// </summary>
        /// <param name="commentEntity">Base entity for removing</param>
        public void Delete(CommentEntity commentEntity)
        {
            commentRepository.Delete(commentEntity.ToDalComment());
            uow.Commit();
        }

        /// <summary>
        /// Updates comment entity
        /// </summary>
        /// <param name="commentEntity">Base entity for updating</param>
        public void Update(CommentEntity commentEntity)
        {
            commentRepository.Update(commentEntity.ToDalComment());
            uow.Commit();
        }

        /// <summary>
        /// Returns all comment entities
        /// </summary>
        /// <returns>Sequence of comments</returns>
        public IEnumerable<CommentEntity> GetAllComments()
        {
            return commentRepository.GetAll().Select(a => a.ToBllComment());
        }

        /// <summary>
        /// Returns comment entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of comment entities</returns
        public IEnumerable<CommentEntity> GetAllByPredicate(Expression<Func<CommentEntity, bool>> f)
        {
            var visitor = new Visitor<CommentEntity, DalComment>(Expression.Parameter(typeof(DalComment), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalComment, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            //ToList()
            return commentRepository.GetAllByPredicate(exp2).Select(comment => comment.ToBllComment()).ToList();
        }

        /// <summary>
        /// Returns comment entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Comment that satisfies predicate</returns>
        public CommentEntity GetOneByPredicate(Expression<Func<CommentEntity, bool>> f)
        {
            var visitor = new Visitor<CommentEntity, DalComment>(Expression.Parameter(typeof(DalArticle), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalComment, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return commentRepository.GetOneByPredicate(exp2).ToBllComment();
        }

        /// <summary>
        /// Returns comment with specified Id
        /// </summary>
        /// <param name="Id">Comment Id</param>
        /// <returns>Comment with specified Id</returns>
        public CommentEntity GetById(int Id)
        {
            return commentRepository.GetById(Id)?.ToBllComment();
        }

    }
}
