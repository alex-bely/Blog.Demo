using BLL.Interfacies.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfacies.Services
{
    /// <summary>
    /// Provides methods for article entities handling
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Returns role with specified Id
        /// </summary>
        /// <param name="Id">Role Id</param>
        /// <returns>Role with specified Id</returns
        RoleEntity GetById(int Id);

        /// <summary>
        /// Creates new role entity
        /// </summary>
        /// <param name="entity">Base entity for new role entity</param>
        void Create(RoleEntity entity);
    }
}
