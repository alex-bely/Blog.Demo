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
    /// Provides methods for article entities handling
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly IUnitOfWork uow;
        private readonly IArticleRepository articleRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ITagRepository tagRepository;

        /// <summary>
        /// Initializes new article service instance
        /// </summary>
        /// <param name="unitOfWork">Unit of work instance for the service</param>
        /// <param name="articleRepository">Article repository instance for articles handling</param>
        /// <param name="commentRepository">Comment repository instance for comments handling</param>
        /// <param name="tagRepository">Tag repository instance for tags handling</param>
        public ArticleService(IUnitOfWork unitOfWork, IArticleRepository articleRepository, ICommentRepository commentRepository, ITagRepository tagRepository)
        {
            this.uow = unitOfWork;
            this.articleRepository = articleRepository;
            this.commentRepository = commentRepository;
            this.tagRepository = tagRepository;
        }

        
        /// <summary>
        /// Creates new article entity with specified tag string
        /// </summary>
        /// <param name="articleEntity">Base entity for new article entity</param>
        /// <param name="tags">String that contains tags</param>
        public void Create(ArticleEntity articleEntity, string tags)
        {
            articleRepository.Create(articleEntity.ToDalArticle(), tags);
            uow.Commit();
        }

        /// <summary>
        /// Deletes article entity
        /// </summary>
        /// <param name="entity">Base entity for removing</param>
        public void Delete(ArticleEntity entity)
        {
            commentRepository.DeleteCommentsOfArticle(entity.Id);
            articleRepository.Delete(entity.ToDalArticle());
            uow.Commit();
            DeleteUnusableTags();
        }

        /// <summary>
        /// Updates article entity with specified tag string
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        /// <param name="tags">String that contains tags</param>
        public void Update(ArticleEntity entity, string tags)
        {
            articleRepository.Update(entity.ToDalArticle(), tags);
            uow.Commit();
            DeleteUnusableTags();
        }

        /// <summary>
        /// Returns article entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of article entities</returns>
        public IEnumerable<ArticleEntity> GetAllByPredicate(Expression<Func<ArticleEntity, bool>> f)
        {
            var visitor = new Visitor<ArticleEntity, DalArticle>(Expression.Parameter(typeof(DalArticle), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalArticle, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            //ToList()
            return articleRepository.GetAllByPredicate(exp2).Select(article => article.ToBllArticle()).ToList();
        }

        /// <summary>
        /// Returns all article entities
        /// </summary>
        /// <returns>Sequence of articles</returns>
        public IEnumerable<ArticleEntity> GetAllArticles()
        {
            return articleRepository.GetAll().Select(a => a.ToBllArticle());
        }

        /// <summary>
        /// Returns article with specified Id
        /// </summary>
        /// <param name="Id">Article Id</param>
        /// <returns>Article with specified Id</returns>
        public ArticleEntity GetById(int Id)
        {
            return articleRepository.GetById(Id)?.ToBllArticle();
        }

        /// <summary>
        /// Returns article entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Article that satisfies predicate</returns>
        public ArticleEntity GetOneByPredicate(Expression<Func<ArticleEntity, bool>> f)
        {
            var visitor = new Visitor<ArticleEntity, DalArticle>(Expression.Parameter(typeof(DalArticle), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalArticle, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return articleRepository.GetOneByPredicate(exp2).ToBllArticle();
        }

        /// <summary>
        /// Returns article entities that contain specified tag
        /// </summary>
        /// <param name="tagName">Tag</param>
        /// <returns>Article entities that contain specified tag</returns>
        public IEnumerable<ArticleEntity> GetArticlesByTagName(string tagName)
        {
            return articleRepository.GetArticlesByTagName(tagName).Select(el=>el.ToBllArticle()).ToList();
        }

        /// <summary>
        /// Removes tags that are not related to any article
        /// </summary>
        private void DeleteUnusableTags()
        {
            var taggs = tagRepository.GetAll();
            var unusableTags = taggs.Where(el => el.Articles.Count == 0);
            foreach (var tag in unusableTags)
                tagRepository.Delete(tag);
            uow.Commit();
        }
    }
}
