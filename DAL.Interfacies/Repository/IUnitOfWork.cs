using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfacies.Repository
{
    /// <summary>
    /// Provides methods of database context handling
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits context changes
        /// </summary>
        void Commit();
    }
}
