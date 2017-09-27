using System;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class UserMappers
    {
        public static DALUser ToDALUser(this User user)
        {
            if (user == null) return null;

            return new DALUser()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public static User ToORMUser(this DALUser user)
        {
            if (user == null) return null;

            return new User()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}