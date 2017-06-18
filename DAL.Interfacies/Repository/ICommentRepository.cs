using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    /// <summary>
    /// Provides methods for comment entities handling
    /// </summary>
    public interface ICommentRepository : IRepository<DalComment>
    {
        /// <summary>
        /// Deletes all comments related to article with specified Id
        /// </summary>
        /// <param name="id">Article Id</param>
        void DeleteCommentsOfArticle(int id);
    }
}
