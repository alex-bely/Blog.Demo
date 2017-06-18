using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using ExpressionTreeVisitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Provides methods for tag entities handling
    /// </summary>
    public class TagService:ITagService
    {
        private readonly IUnitOfWork uow;
        private readonly ITagRepository tagRepository;
        private readonly IArticleRepository articleRepository;

        /// <summary>
        /// Initializes new tag service instance
        /// </summary>
        /// <param name="tagRepository">Tag repository instance for tags handling</param>
        /// <param name="articleRepository">Article repository instance for articles handling</param>
        /// <param name="uow">Unit of work instance for the service</param>
        public TagService(ITagRepository tagRepository, IArticleRepository articleRepository, IUnitOfWork uow)
        {
            this.tagRepository = tagRepository;
            this.uow = uow;
            this.articleRepository = articleRepository;
        }

        /// <summary>
        /// Creates new tag entity
        /// </summary>
        /// <param name="entity">Base entity for new tag</param>
        public void Create(TagEntity entity)
        {
            tagRepository.Create(entity.ToDalTag());
            uow.Commit();
        }

        /// <summary>
        /// Updates tag entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        public void Update(TagEntity entity)
        {
            tagRepository.Update(entity.ToDalTag());
            uow.Commit();
        }

        /// <summary>
        /// Deletes tag entity
        /// </summary>
        /// <param name="entity">Base entity for removing</param>
        public void Delete(TagEntity entity)
        {
            tagRepository.Delete(entity.ToDalTag());
            uow.Commit();
        }

        /// <summary>
        /// Returns all tag entities
        /// </summary>
        /// <returns>List of tags</returns>
        public IEnumerable<TagEntity> GetAll()
        {
            return tagRepository.GetAll().Select(el=>el.ToBllTag()).ToList();
        }

        /// <summary>
        /// Returns tag entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of tag entities</returns>
        public IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> f)
        {
            var visitor = new Visitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            //ToList()
            return tagRepository.GetAllByPredicate(exp2).Select(tag => tag.ToBllTag()).ToList();
        }

        /// <summary>
        /// Returns tag with specified Id key
        /// </summary>
        /// <param name="key">Tag Id</param>
        /// <returns>Tag with specified Id</returns>
        public TagEntity GetById(int key)
        {
            var tag = tagRepository.GetById(key).ToBllTag();
            return tag;
        }

        /// <summary>
        /// Returns tag entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Tag that satisfies predicate</returns>
        public TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        /// <summary>
        /// Returns tag entities that are contained in the article with specified id
        /// </summary>
        /// <param name="articleId">Article Id</param>
        /// <returns>Tag entities that are contained in the article with specified id</returns>
        public IEnumerable<TagEntity> GetTagsByArticleId(int articleId)
        {
            var tags = tagRepository.GetTagsByPostId(articleId).Select(el => el.ToBllTag()).ToList();
            return tags;
        }
        
    }
}
