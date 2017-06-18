using DAL.Interfacies.Repository;
using DAL.Mappers;
using System;
using DAL.Interfacies.DTO;
using System.Data.Entity;
using ORM;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using ExpressionTreeVisitor;

namespace DAL.Concrete
{
    /// <summary>
    /// Provides methods for user entities handling
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new user repository instance
        /// </summary>
        /// <param name="dbContext">Database context instance for the repository</param>
        public UserRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException();
            }
            context = dbContext;
        }

        /// <summary>
        /// Creates new user entity
        /// </summary>
        /// <param name="e">Base entity for new user entity</param>
        public void Create(DalUser e)
        {
            Role role;
            if (e.Roles.Count != 0)
            {
                var roleName = e.Roles.FirstOrDefault().Name;
                role = context.Set<Role>().FirstOrDefault(r => r.Name == roleName);
                e.Roles = new List<DalRole>();
            }
            else
            {
                role = context.Set<Role>().FirstOrDefault(r => r.Name == "user");
            }

            var user = e.ToORmUser();
            user.Roles.Add(role);

            context.Set<User>().Add(user);
        }

        /// <summary>
        /// Deletes user entity
        /// </summary>
        /// <param name="e">Base entity for removing</param>
        public void Delete(DalUser e)
        {
            var user = context.Set<User>().Single(u => u.Id == e.Id);
            context.Set<User>().Remove(user);
        }

        /// <summary>
        /// Returns all user entities
        /// </summary>
        /// <returns>Sequence of users</returns>
        public IEnumerable<DalUser> GetAll()
        {
            return context.Set<User>().Include(u=>u.Roles).ToList().Select(user => user.ToDalUser());
        }

        /// <summary>
        /// Returns user entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of user entities</returns>
        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var visitor = new Visitor<DalUser, User>(Expression.Parameter(typeof(User), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<User, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            var x = context.Set<User>().Include(user => user.Roles).Where(exp2).ToList();
            return x.Select(user => user.ToDalUser());
        }


        /// <summary>
        /// Returns user with specified Id key
        /// </summary>
        /// <param name="key">User Id</param>
        /// <returns>User with specified Id</returns>
        public DalUser GetById(int key)
        {
            var ormUser = context.Set<User>().Include(u => u.Roles).FirstOrDefault(u => u.Id == key);
            return ormUser == null ? null : ormUser.ToDalUser();
        }

        /// <summary>
        /// Returns user entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>User that satisfies predicate</returns>
        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> f)
        {
            return GetAllByPredicate(f).FirstOrDefault();
        }

        /// <summary>
        /// Updates user entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        public void Update(DalUser entity)
        {
            var ormUser = context.Set<User>().Where(a => a.Id == entity.Id).FirstOrDefault();
            context.Set<User>().Attach(ormUser);
            ormUser.Roles.Clear();
            ormUser.Roles = new List<Role>();
            ormUser.Email = entity.Email;
            FromRolesToOrmUserRoles(ormUser, entity.Roles);
            ormUser.Avatar = entity.Avatar;
        }

        /// <summary>
        /// Adds roles to user entity from specified list of roles
        /// </summary>
        /// <param name="user">User entity roles are added to</param>
        /// <param name="roles">Specified list of roles</param>
        private void FromRolesToOrmUserRoles(User user, List<DalRole> roles)
        {
            foreach (var i in roles)
            {
                Role role = context.Set<Role>().FirstOrDefault(r => r.Name == i.Name);
                user.Roles.Add(role);
            }
        }


    }
}
