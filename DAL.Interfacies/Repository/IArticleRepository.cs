using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    /// <summary>
    /// Provides methods for article entities handling
    /// </summary>
    public interface IArticleRepository : IRepository<DalArticle>
    {
        /// <summary>
        /// Creates new article entity with specified tag string
        /// </summary>
        /// <param name="dalArticle">Base entity for new article entity</param>
        /// <param name="tags">String that contains tags</param
        void Create(DalArticle dalArticle, string tags);

        /// <summary>
        /// Updates article entity with specified tag string
        /// </summary>
        /// <param name="dalArticle">Base entity for updating</param>
        /// <param name="tags">String that contains tags</param>
        void Update(DalArticle dalArticle, string tags);

        /// <summary>
        /// Returns article entities that contain specified tag
        /// </summary>
        /// <param name="tagName">Tag</param>
        /// <returns>Article entities that contain specified tag</returns>
        IEnumerable<DalArticle> GetArticlesByTagName(string tagName);
    }
}
