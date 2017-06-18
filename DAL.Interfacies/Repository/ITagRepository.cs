using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    /// <summary>
    /// Provides methods for tag entities handling
    /// </summary>
    public interface ITagRepository : IRepository<DalTag>
    {
        /// <summary>
        /// Returns tags of the article with specified id.
        /// </summary>
        /// <param name="postId">Article Id</param>
        /// <returns>Collection of tags.</returns>
        IEnumerable<DalTag> GetTagsByPostId(int postId);
    }
}
