using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using System.Linq.Expressions;

namespace BLL.Interfacies.Services
{
    /// <summary>
    /// Provides methods for article entities handling
    /// </summary>
    public interface IArticleService
    {
        /// <summary>
        /// Creates new article entity with specified tag string
        /// </summary>
        /// <param name="articleEntity">Base entity for new article entity</param>
        /// <param name="tags">String that contains tags</param>
        void Create(ArticleEntity articleEntity, string tags);

        /// <summary>
        /// Updates article entity with specified tag string
        /// </summary>
        /// <param name="p">Base entity for updating</param>
        /// <param name="tags">String that contains tags</param>
        void Update(ArticleEntity p, string tags);

        /// <summary>
        /// Deletes article entity
        /// </summary>
        /// <param name="p">Base entity for removing</param>
        void Delete(ArticleEntity p);

        /// <summary>
        /// Returns article entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Article that satisfies predicate</returns>
        ArticleEntity GetOneByPredicate(Expression<Func<ArticleEntity, bool>> f);

        /// <summary>
        /// Returns article entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of article entities</returns>
        IEnumerable<ArticleEntity> GetAllByPredicate(Expression<Func<ArticleEntity, bool>> f);

        /// <summary>
        /// Returns all article entities
        /// </summary>
        /// <returns>Sequence of articles</returns>
        IEnumerable<ArticleEntity> GetAllArticles();

        /// <summary>
        /// Returns article entities that contain specified tag
        /// </summary>
        /// <param name="tagName">Tag</param>
        /// <returns>Article entities that contain specified tag</returns>
        IEnumerable<ArticleEntity> GetArticlesByTagName(string tagName);

    }
}
