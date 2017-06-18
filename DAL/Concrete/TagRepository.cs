using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using DAL.Mappers;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Data.Entity;
using ExpressionTreeVisitor;

namespace DAL.Concrete
{
    /// <summary>
    /// Provides methods for tag entities handling
    /// </summary>
    public class TagRepository : ITagRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new tag repository instance
        /// </summary>
        /// <param name="context">Database context instance for the repository</param>
        public TagRepository(DbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Creates new tag entity
        /// </summary>
        /// <param name="e">Base entity for new tag</param>
        public void Create(DalTag e)
        {
            context.Set<Tag>().Add(e.ToOrmTag());
        }

        /// <summary>
        /// Deletes tag entity
        /// </summary>
        /// <param name="e">Base entity for removing</param>
        public void Delete(DalTag e)
        {
            var tag = context.Set<Tag>().Where(a => a.Id == e.Id).FirstOrDefault();
            if (tag != null)
            {
                context.Set<Tag>().Remove(tag);
            }
        }

        /// <summary>
        /// Updates tag entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        public void Update(DalTag entity)
        {
            var tag = context.Set<Tag>().SingleOrDefault(t => t.Id == entity.Id);

            if (tag != null)
                tag.Name = entity.Name;
        }

        /// <summary>
        /// Returns all tag entities
        /// </summary>
        /// <returns>List of tags</returns>
        public IEnumerable<DalTag> GetAll()
        {
            var tags = context.Set<Tag>().Include(u => u.Articles).ToList();
            return tags.Select(u => u.ToDalTag()).ToList();
        }

        /// <summary>
        /// Returns tag entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of tag entities</returns>
        public IEnumerable<DalTag> GetAllByPredicate(Expression<Func<DalTag, bool>> f)
        {
            var visitor = new Visitor<DalTag, Tag>(Expression.Parameter(typeof(Tag), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Tag, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var x = context.Set<Tag>().Where(exp2).Include(Tag=>Tag.Articles).ToList();
            return x.Select(u => u.ToDalTag());

        }

        /// <summary>
        /// Returns tag with specified Id key
        /// </summary>
        /// <param name="key">Tag Id</param>
        /// <returns>Tag with specified Id</returns
        public DalTag GetById(int key)
        {
            var ormTag = context.Set<Tag>().Include(u => u.Articles).FirstOrDefault(u => u.Id == key);
            return ormTag == null ? null : ormTag.ToDalTag();
        }

        /// <summary>
        /// Returns tag entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Tag that satisfies predicate</returns>
        public DalTag GetOneByPredicate(Expression<Func<DalTag, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        /// <summary>
        /// Returns tag entities that are contained in the article with specified id
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>Tag entities that are contained in the article with specified id</returns>
        public IEnumerable<DalTag> GetTagsByPostId(int articleId)
        {
            var article = context.Set<Article>().Where(el => el.Id == articleId).FirstOrDefault();
            var tags = article.Tags.Select(el => el.ToDalTag()).ToList();
            return tags;
        }

        
    }
}
