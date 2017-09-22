using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class RoleMappers
    {
        public static Role ToRole(this DALRole role)
        {
            return new Role()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DALRole ToDALRole(this Role role)
        {
            return new DALRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
            
    }
}