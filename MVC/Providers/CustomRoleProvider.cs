using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interfaces.Interfaces;

namespace MVC.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof (IUserService));

        public override bool IsUserInRole(string login, string roleName)
        {

            var user = UserService.GetByLogin(login);

            if (user == null) return false;

            var userRole = UserService.GetUserRoles(user.Id)
                .FirstOrDefault(r => String.Equals(roleName, r.Name, StringComparison.InvariantCultureIgnoreCase));

            if (userRole != null)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string login)
        {
            var user = UserService.GetByLogin(login);
            return UserService.GetUserRoles(user.Id).Select(r => r.Name).ToArray();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}