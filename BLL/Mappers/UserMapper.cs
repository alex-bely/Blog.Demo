using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToBllUser(this DalUser dalEntity)
        {
            if (dalEntity == null)
                return null;
            return new UserEntity()
            {
                Id = dalEntity.Id,
                Login = dalEntity.Login,
                Email = dalEntity.Email,
                Password = dalEntity.Password,
                RegistrationDate = dalEntity.RegistrationDate,
                Avatar = dalEntity.Avatar,
                Roles = dalEntity.Roles.Select(r => r.ToBllRole()).ToList()
            };
        }

        public static DalUser ToDalUser(this UserEntity bllEntity)
        {
            return new DalUser()
            {
                Id = bllEntity.Id,
                Login = bllEntity.Login,
                Email = bllEntity.Email,
                Password = bllEntity.Password,
                RegistrationDate = bllEntity.RegistrationDate,
                Avatar = bllEntity.Avatar,
                Roles = bllEntity.Roles != null
                        ? bllEntity.Roles.Select(r => r.ToDalRole()).ToList()
                        : new List<DalRole>()
            };
        }
    }
}
