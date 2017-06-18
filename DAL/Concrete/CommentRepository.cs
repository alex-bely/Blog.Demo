using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DAL.Mappers;
using ExpressionTreeVisitor;

namespace DAL.Concrete
{
    /// <summary>
    /// Provides methods for comment entities handling
    /// </summary>
    public class CommentRepository : ICommentRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new comment repository instance
        /// </summary>
        /// <param name="dbContext">Database context instance for the repository</param>
        public CommentRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("entitiesContex");
            }
            this.context = dbContext;
        }

        /// <summary>
        /// Creates new comment entity
        /// </summary>
        /// <param name="e">Base entity for new comment entity</param>
        public void Create(DalComment e)
        {
            context.Set<Comment>().Add(e.ToOrmComment());
        }

        /// <summary>
        /// Deletes comment entity
        /// </summary>
        /// <param name="e">Base entity for removing</param>
        public void Delete(DalComment e)
        {
            var comment = context.Set<Comment>().Where(a => a.Id == e.Id).FirstOrDefault();
            if (comment != null)
            {
                context.Set<Comment>().Remove(comment);
            }
            //context.Set<Comment>().Remove(e.ToOrmComment());
        }

        /// <summary>
        /// Updates comment entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        public void Update(DalComment entity)
        {
            
            var comment = context.Set<Comment>().Where(a => a.Id == entity.Id).FirstOrDefault();
            context.Set<Comment>().Attach(comment);
            if (entity.Text != null) comment.Text = entity.Text;
            if (entity.PublicationDate != null) comment.PublicationDate = entity.PublicationDate;
        }

        /// <summary>
        /// Returns all comment entities
        /// </summary>
        /// <returns>Sequence of comments</returns
        public IEnumerable<DalComment> GetAll()
        {
            var comments = context.Set<Comment>().ToList();
            return comments.Select(u => u.ToDalComment());
        }

        /// <summary>
        /// Returns comment entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of comment entities</returns
        public IEnumerable<DalComment> GetAllByPredicate(Expression<Func<DalComment, bool>> f)
        {
            var visitor = new Visitor<DalComment, Comment>(Expression.Parameter(typeof(Comment), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Comment, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var x = context.Set<Comment>().Where(exp2).ToList();
            return x.Select(u => u.ToDalComment());
        }

        /// <summary>
        /// Returns comment with specified Id key
        /// </summary>
        /// <param name="key">Comment Id</param>
        /// <returns>Comment with specified Id</returns>
        public DalComment GetById(int key)
        {
            var ormComment = context.Set<Comment>().FirstOrDefault(u => u.Id == key);
            return ormComment == null ? null : ormComment.ToDalComment();
        }

        /// <summary>
        /// Returns comment entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Comment that satisfies predicate</returns>
        public DalComment GetOneByPredicate(Expression<Func<DalComment, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        /// <summary>
        /// Deletes all comments related to article with specified Id
        /// </summary>
        /// <param name="id">Article Id</param>
        public void DeleteCommentsOfArticle(int id)
        {
            var comments = context.Set<Comment>().Where(comment => comment.ArticleId == id).ToList();
            context.Set<Comment>().RemoveRange(comments);
        }
    }
}
