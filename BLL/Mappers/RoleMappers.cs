using BLL.Interfaces.BLLEntities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class RoleMappers
    {
        public static BLLRole ToBLLRole(this DALRole role)
        {
            if (role == null) return null;

            return new BLLRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DALRole ToDALRole(this BLLRole role)
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