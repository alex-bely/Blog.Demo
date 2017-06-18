using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Blog.Providers
{
    public class CustomProfileProvider : ProfileProvider
    {
        private IUserService UserService
           => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            var result = new SettingsPropertyValueCollection();

            if (collection == null || collection.Count < 1 || context == null)
            {
                return result;
            }

            var userName = (string)context["UserName"];
            if (String.IsNullOrEmpty(userName))
            {
                return result;
            }

            UserEntity user = UserService.GetOneByPredicate(u => u.Login == userName);
            if (userName != null)
            {
                foreach (SettingsProperty property in collection)
                {
                    var spv = new SettingsPropertyValue(property)
                    {
                        PropertyValue = user.GetType().GetProperty(property.Name).GetValue(user, null)
                    };
                    result.Add(spv);
                }
            }
            else
            {
                foreach (SettingsProperty property in collection)
                {
                    var svp = new SettingsPropertyValue(property) { PropertyValue = null };
                    result.Add(svp);
                }
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            var userName = (string)context["Login"];

            if (string.IsNullOrEmpty(userName) || collection.Count < 1)
            {
                return;
            }
            UserEntity user = UserService.GetOneByPredicate(u => u.Login == userName);
            if (userName != null)
            {
                foreach (SettingsPropertyValue value in collection)
                {
                    user.GetType().GetProperty(value.Property.Name).SetValue(user, value.PropertyValue);
                }
            }
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

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }              

        #endregion
    }
}