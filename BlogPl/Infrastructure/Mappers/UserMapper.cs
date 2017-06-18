using BLL.Interfacies.Entities;
using BlogPL.Models.AccountViewModel;
using BlogPL.Models.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogPL.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserEntity ToBllUser(this RegisterViewModel user)
        {
            return new UserEntity()
            {
                Id = user.UserId,
                Login = user.UserName,
                Email = user.UserEmail
            };
        }

        public static RegisterViewModel ToMvcUser(this UserEntity user)
        {
            return new RegisterViewModel()
            {
                UserId = user.Id,
                UserName = user.Login,
                UserEmail = user.Email
            };
        }


        public static UserViewModel ToMvcInfoUser(this UserEntity user)
        {
            return new UserViewModel()
            {
                UserId = user.Id,
                Login = user.Login,
                UserEmail = user.Email,
                RegistrationDate=user.RegistrationDate.Value,
                Roles=user.Roles.ToSrtingRoles(),
                //Avatar=user.Avatar
                //ArticlesCount=user.Articles.Count
            };
        }


        private static string ToSrtingRoles(this List<RoleEntity> roles)
        {
            string mvcRoles = String.Empty;
            
            foreach (var role in roles.OrderBy(el => el.Name))
                mvcRoles += role.Name+" ";
            return mvcRoles.TrimEnd(' ');
        }
    }
}