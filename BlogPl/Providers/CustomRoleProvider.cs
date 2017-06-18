using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BlogPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        private IUserService UserService
           => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
        private IRoleService RoleService
           => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override void CreateRole(string roleName)
        {
            RoleService.Create(new RoleEntity() { Name = roleName });
        }

        public override string[] GetRolesForUser(string username)
        {
            List<string> roles = new List<string>();
            var user = UserService.GetOneByPredicate(el => el.Login == username);
            if (user == null) return roles.ToArray();
            var userRole = user.Roles;
            if (userRole != null)
            {
                foreach (var role in userRole)
                {
                    roles.Add(role.Name);
                }
            }
            return roles.ToArray();
        }

        public override bool IsUserInRole(string username, string rolename)
        {
            bool outputResult = false;
            var user = UserService.GetOneByPredicate(el => el.Login == username);
            if (user != null)
            {
                var role = user.Roles.FirstOrDefault();
                if (role != null && role.Name == rolename)
                    outputResult = true;
            }
            return outputResult;
        }

        #region Stabs
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}