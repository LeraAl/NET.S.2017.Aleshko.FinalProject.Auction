using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class RoleMappers
    {
        public static Role ToRole(this DALRole role)
        {
            if (role == null) return null;

            return new Role()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DALRole ToDALRole(this Role role)
        {
            if (role == null) return null;

            return new DALRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
            
    }
}