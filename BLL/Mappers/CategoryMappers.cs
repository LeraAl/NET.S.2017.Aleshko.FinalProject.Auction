using BLL.Interfaces.BLLEntities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class CategoryMappers
    {
        public static BLLCategory ToBLLCategory(this DALCategory category)
        {
            return new BLLCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static DALCategory ToDALCategory(this BLLCategory category)
        {
            return new DALCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}