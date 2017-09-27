using BLL.Interfaces.BLLEntities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class UserMappers
    {
        public static BLLUser ToBLLUser(this DALUser user)
        {
            return new BLLUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password
            };
        }

        public static DALUser ToDALUser(this BLLUser user)
        {
            return new DALUser()
            {
                Id = user.Id,
                Login = user.Login,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password
            };
        }
    }
}