using System.Diagnostics;
using System.Web.Helpers;
using BLL.Interfaces.BLLEntities;
using MVC.Models;
using MVC.Models.Account;
using MVC.Models.Profile;

namespace MVC.Infrasrtucture.Mappers
{
    public static class UserMappers
    {
        public static BLLUser ToBLLUser(this RegisterModel model)
        {
            return new BLLUser()
            {
                Login = model.Login,
                Email = model.Email,
                Password = Crypto.HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        public static ProfileViewModel ToProfileVM(this BLLUser model)
        {
            return new ProfileViewModel()
            {
                Login = model.Login,
                Email = model.Email,
                Name = $"{model.FirstName} {model.LastName}" 
            };
        }

        public static ProfileEditModel ToProfileEditModel(this BLLUser model)
        {
            return new ProfileEditModel()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }
    }
}