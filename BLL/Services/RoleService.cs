using BLL.Interfacies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfacies.Entities;
using DAL.Interfacies.Repository;
using BLL.Mappers;

namespace BLL.Services
{
    /// <summary>
    /// Provides methods for role entities handling
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;

        /// <summary>
        /// Initializes new role service instance
        /// </summary>
        /// <param name="uow">Unit of work instance for the service</param>
        /// <param name="repository">Role repository instance for roles handling</param>
        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }

        /// <summary>
        /// Creates new role entity
        /// </summary>
        /// <param name="entity">Base entity for new role entity</param>
        public void Create(RoleEntity entity)
        {
            roleRepository.Create(entity.ToDalRole());
            uow.Commit();
        }

        /// <summary>
        /// Deletes role entity
        /// </summary>
        /// <param name="entity">Base entity for removing</param>
        public void Delete(RoleEntity entity)
        {
            roleRepository.Delete(entity.ToDalRole());
            uow.Commit();
        }

        /// <summary>
        /// Updates role entity
        /// </summary>
        /// <param name="entity">Base entity for updating</param>
        public void Update(RoleEntity entity)
        {
            roleRepository.Update(entity.ToDalRole());
            uow.Commit();
        }

        /// <summary>
        /// Returns all role entities
        /// </summary>
        /// <returns>Sequence of roles</returns>
        public IEnumerable<RoleEntity> GetAll()
        {
            return roleRepository.GetAll().Select(r => r.ToBllRole());
        }

        /// <summary>
        /// Returns role with specified Id
        /// </summary>
        /// <param name="Id">Role Id</param>
        /// <returns>Role with specified Id</returns>
        public RoleEntity GetById(int Id)
        {
            return roleRepository.GetById(Id)?.ToBllRole();
        }

        
    }
}
