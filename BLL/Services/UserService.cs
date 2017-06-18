using BLL.Interfacies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using System.Linq.Expressions;
using DAL.Interfacies.Repository;
using DAL.Interfacies.DTO;
using BLL.Mappers;
using System.Web.Helpers;
using ExpressionTreeVisitor;
using System.Drawing;

namespace BLL.Services
{
    /// <summary>
    /// Provides methods for user entities handling
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Initializes new user service instance
        /// </summary>
        /// <param name="uow">Unit of work instance for the service</param>
        /// <param name="userRepository">User repository instance for users handling</param>
        /// <param name="roleRepository">Role repository instance for roles handling</param>
        public UserService(IUnitOfWork uow, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.uow = uow;
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Creates new user entity
        /// </summary>
        /// <param name="user">Base entity for new user entity</param>
        public void CreateUser(UserEntity user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
	        user.Password = Crypto.HashPassword(user.Password);
            user.RegistrationDate = DateTime.Now;
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        /// <summary>
        /// Deletes user entity
        /// </summary>
        /// <param name="user">Base entity for removing</param>
        public void DeleteUser(UserEntity user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        /// <summary>
        /// Updates user entity
        /// </summary>
        /// <param name="user">Base entity for updating</param>
        public void UpdateUser(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }

        /// <summary>
        /// Returns user with specified Id
        /// </summary>
        /// <param name="Id">User Id</param>
        /// <returns>User with specified Id</returns>
        public UserEntity GetUserById(int Id)
        {
            return userRepository.GetById(Id).ToBllUser();
        }

        /// <summary>
        /// Returns all user entities
        /// </summary>
        /// <returns>Sequence of users</returns>
        public IEnumerable<UserEntity> GetAllUsers()
        {
            return userRepository.GetAll().Select(u => u.ToBllUser()).ToList();
        }

        /// <summary>
        /// Returns user entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>User that satisfies predicate</returns>
        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> f)
        {
            var visitor = new Visitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            return userRepository.GetOneByPredicate(exp2).ToBllUser();
        }

        /// <summary>
        /// Returns user entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of user entities</returns>
        public IEnumerable<UserEntity> GetUsersByPredicate(Expression<Func<UserEntity, bool>> f)
        {
            var visitor = new Visitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), f.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(f.Body), visitor.NewParameterExp);
            //ToList()
            return userRepository.GetAllByPredicate(exp2).Select(user => user.ToBllUser()).ToList();
        }

        /// <summary>
        /// Returns base64 image-containing string of avatar of the user with specified login
        /// </summary>
        /// <param name="Login">User login</param>
        /// <returns>Base64 image-containing string </returns>
        public string GetAvatarAsString(string Login)
        {
            ImageConverter converter = new ImageConverter();
            var t = userRepository.GetOneByPredicate(el => el.Login == Login);
            string base64String = Convert.ToBase64String(t.Avatar);
            string imageDataURL = string.Format("data:image/png;base64,{0}", base64String);
            return imageDataURL;

        }

    }
}
