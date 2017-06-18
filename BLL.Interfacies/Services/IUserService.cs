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
    /// Provides methods for user entities handling
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Creates new user entity
        /// </summary>
        /// <param name="user">Base entity for new user entity</param>
        void CreateUser(UserEntity user);

        /// <summary>
        /// Deletes user entity
        /// </summary>
        /// <param name="user">Base entity for removing</param>
        void DeleteUser(UserEntity user);

        /// <summary>
        /// Updates user entity
        /// </summary>
        /// <param name="user">Base entity for updating</param>
        void UpdateUser(UserEntity user);

        /// <summary>
        /// Returns user with specified Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User with specified Id</returns>
        UserEntity GetUserById(int id);

        /// <summary>
        /// Returns user entitiy requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>User that satisfies predicate</returns>
        UserEntity GetOneByPredicate(System.Linq.Expressions.Expression<Func<UserEntity, bool>> f);

        /// <summary>
        /// Returns all user entities
        /// </summary>
        /// <returns>Sequence of users</returns>
        IEnumerable<UserEntity> GetAllUsers();

        /// <summary>
        /// Returns user entities requested with specified predicate
        /// </summary>
        /// <param name="f">Predicate for selecting</param>
        /// <returns>List of user entities</returns>
        IEnumerable<UserEntity> GetUsersByPredicate(Expression<Func<UserEntity, bool>> f);

        /// <summary>
        /// Returns base64 image-containing string of avatar of the user with specified login
        /// </summary>
        /// <param name="Login">User login</param>
        /// <returns>Base64 image-containing string </returns
        string GetAvatarAsString(string Login);
    }
}
