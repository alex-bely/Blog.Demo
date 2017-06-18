using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository;
using DAL.Mappers;
using ExpressionTreeVisitor;
using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Concrete
{
    /// <summary>
    /// Provides methods for role entities handling
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new role repository instance
        /// </summary>
        /// <param name="dbContext">Database context instance for the repository</param>
        public RoleRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("entitiesContext");
            }
            this.context = dbContext;
        }

        /// <summary>
        /// Creates new role entity
        /// </summary>
        /// <param name="e">Base entity for new role entity</param>
        public void Create(DalRole e)
        {
            context.Set<Role>().Add(e.ToOrmRole());
        }

        /// <summary>
        /// Deletes role entity
        /// </summary>
        /// <param name="e">Base entity for removing</param>
        public void Delete(DalRole e)
        {
            context.Set<Role>().Remove(e.ToOrmRole());
        }

        /// <summary>
        /// Updates role entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        public void Update(DalRole entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var role = entity.ToOrmRole();
            context.Entry(role).State = EntityState.Modified;
        }

        /// <summary>
        /// Returns all role entities
        /// </summary>
        /// <returns>Sequence of roles</returns>
        public IEnumerable<DalRole> GetAll()
        {
            return context.Set<Role>().ToList().Select(r => r.ToDalRole());
        }

        /// <summary>
        /// Returns role entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of role entities</returns>
        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> f)
        {
            var visitor = new Visitor<DalRole, Role>(Expression.Parameter(typeof(Role), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<Role, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return context.Set<Role>()
                .Where(exp2)
                .Select(r => r.ToDalRole());
        }

        /// <summary>
        /// Returns role with specified Id key
        /// </summary>
        /// <param name="key">Role Id</param>
        /// <returns>Role with specified Id</returns>
        public DalRole GetById(int key)
        {
            return context.Set<Role>().FirstOrDefault(r => r.Id == key)?.ToDalRole();
        }

        /// <summary>
        /// Returns role entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>Role that satisfies predicate</returns>
        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        
    }
}
