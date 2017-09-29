using System.Diagnostics;
using System.Web.Helpers;
using BLL.Interfaces.BLLEntities;
using MVC.Models.Account;

namespace MVC.Infrasrtucture.Mappers
{
    public static class UserMappers
    {
        public static BLLUser ToBLLUser(this RegisterModel model)
        {
            Debug.WriteLine(Crypto.HashPassword(model.Password));
            Debug.WriteLine(Crypto.HashPassword(model.Password).Length);
            return new BLLUser()
            {
                Login = model.Login,
                Email = model.Email,
                Password = Crypto.HashPassword(model.Password),
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }
    }
}