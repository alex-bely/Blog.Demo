using BLL.Interfacies.Entities;
using MVcPL.Models.AccountViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVcPL.Infrastructure.Mappers
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
    }
}