using DAL.Interfacies.Repository;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL.Concrete
{
    /// <summary>
    /// Provides methods of database context handling
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        /// <summary>
        /// Initializes new Unit of work instance
        /// </summary>
        /// <param name="context">Database context instance for the repository</param>
        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Commits context changes
        /// </summary>
        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Releases context
        /// </summary>
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
