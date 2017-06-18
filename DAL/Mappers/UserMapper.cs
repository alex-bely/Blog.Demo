using DAL.Interfacies.DTO;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this User ormEntity)
        {
            //if (ormEntity == null)
            //    return null;
            return new DalUser()
            {
                Id = ormEntity.Id,
                Login = ormEntity.Login,
                Email = ormEntity.Email,
                Password = ormEntity.Password,
                RegistrationDate = ormEntity.RegistrationDate,
                Avatar = ormEntity.Avatar,
                Roles = ormEntity.Roles.Select(r => r.ToDalRole()).ToList()
            };
        }

        public static User ToORmUser(this DalUser dalEntity)
        {
            //if (dalEntity == null)
            //    return null;
            return new User()
            {
                Id = dalEntity.Id,
                Login = dalEntity.Login,
                Email = dalEntity.Email,
                Password = dalEntity.Password,
                RegistrationDate = dalEntity.RegistrationDate,
                Avatar = dalEntity.Avatar,
                Roles =
                    dalEntity.Roles != null
                        ? dalEntity.Roles.Select(r => r.ToOrmRole()).ToList()
                        : null
                //Roles = dalEntity.Roles.Select(r => r.ToOrmRole()).ToList()
            };
        }
    }
}
