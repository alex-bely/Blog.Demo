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
    /// Provides methods for article entities handling
    /// </summary
    public class ArticleRepository : IArticleRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new article repository instance
        /// </summary>
        /// <param name="dbContext">Database context instance for repository</param>
        public ArticleRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("Context is null");
            }
            this.context = dbContext;
        }

        /// <summary>
        /// Creates new article entity with specified tag string
        /// </summary>
        /// <param name="e">Base entity for new article entity</param>
        /// <param name="tags">String that contains tags</param>
        public void Create(DalArticle e, string tags=null)
        {
            var article = e.ToOrmArticle();
            if(tags!=null)
            {
                article.Tags = new List<Tag>();
                FromTagStringToTaggs(article, tags);
            }
                
            context.Set<Article>().Add(article);
        }

        /// <summary>
        /// Deletes article entity
        /// </summary>
        /// <param name="entity">Base entity for removing</param>
        public void Delete(DalArticle e)
        {
            var article = context.Set<Article>().Where(a => a.Id == e.Id).Include(a => a.Tags).FirstOrDefault();
            if (article != null)
            {
                context.Set<Article>().Remove(article);
            }
            //context.Set<Article>().Remove(e.ToOrmArticle());
        }

        /// <summary>
        /// Updates article entity with specified tag string
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        /// <param name="tags">String that contains tags</param>
        public void Update(DalArticle entity, string tags=null)
        {
            var article = context.Set<Article>().Where(a => a.Id == entity.Id).FirstOrDefault();
            context.Set<Article>().Attach(article);
           
            if (entity.Title != null) article.Title = entity.Title;
            if (entity.Content != null) article.Content = entity.Content;
            if (entity.PublicationDate != null) article.PublicationDate = entity.PublicationDate;
            article.Tags.Clear();
            article.Rating = entity.Rating;

            if(tags!=null)
            {
                article.Tags = new List<Tag>();
                FromTagStringToTaggs(article, tags);
            }
        }


        /// <summary>
        /// Returns all article entities
        /// </summary>
        /// <returns>Sequence of articles</returns>
        public IEnumerable<DalArticle> GetAll()
        {
            var articles = context.Set<Article>().Include(u => u.Comments).Include(u => u.Tags).ToList();
            return articles.Select(u => u.ToDalArticle());
        }

        /// <summary>
        /// Returns article entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of article entities</returns>
        public IEnumerable<DalArticle> GetAllByPredicate(Expression<Func<DalArticle, bool>> f)
        {
            var visitor = new Visitor<DalArticle, Article>(Expression.Parameter(typeof(Article), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Article, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var x = context.Set<Article>().Where(exp2).Include(el => el.Tags).ToList();
            return x.Select(u => u.ToDalArticle());
        }


        /// <summary>
        /// Returns article with specified Id key
        /// </summary>
        /// <param name="key">Article Id</param>
        /// <returns>Article with specified Id</returns
        public DalArticle GetById(int key)
        {
            var ormArticle = context.Set<Article>().Include(u => u.Comments).Include(u=>u.Tags).FirstOrDefault(u => u.Id == key);
            return ormArticle == null ? null : ormArticle.ToDalArticle();
        }

        /// <summary>
        /// Returns article entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Article that satisfies predicate</returns>
        public DalArticle GetOneByPredicate(Expression<Func<DalArticle, bool>> f)
        {
            var article= GetAllByPredicate(f).FirstOrDefault();
            return article;
        }

        /// <summary>
        /// Returns article entities that contain specified tag
        /// </summary>
        /// <param name="tagName">Tag</param>
        /// <returns>Article entities that contain specified tag</returns>
        public IEnumerable<DalArticle> GetArticlesByTagName(string tagName)
        {
            var articles = context.Set<Article>().Where(u=>u.Tags.Any(el=>el.Name==tagName)).ToList();
            return articles.Select(u => u.ToDalArticle());
        }

        /// <summary>
        /// Creates new article entity
        /// </summary>
        /// <param name="e">Base entity for new article entity</param>
        void IRepository<DalArticle>.Create(DalArticle e)
        {
            Create(e, null);
        }

        /// <summary>
        /// Updates article entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        void IRepository<DalArticle>.Update(DalArticle entity)
        {
            Update(entity, null);
        }

        /// <summary>
        /// Adds to article tag list tags from the string
        /// </summary>
        /// <param name="article">Article entity tags are added to</param>
        /// <param name="tags">String with tags</param>
        private void FromTagStringToTaggs(Article article, string tags)
        {
            var Tags = tags.Split(new char[] { ' ', ',', '#', ';' }, StringSplitOptions.RemoveEmptyEntries);
            Tags = Tags.Distinct<string>().ToArray<string>();
            foreach (var i in Tags)
            {
                Tag tag = context.Set<Tag>().FirstOrDefault(r => r.Name == i);
                if (tag != null)
                    article.Tags.Add(tag);
                else article.Tags.Add(new Tag { Name = i });
            }
        }
    }
}
