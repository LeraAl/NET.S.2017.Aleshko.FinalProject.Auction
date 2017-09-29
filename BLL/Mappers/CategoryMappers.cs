using BLL.Interfaces.BLLEntities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class CategoryMappers
    {
        public static BLLCategory ToBLLCategory(this DALCategory category)
        {
            if (category == null) return null;

            return new BLLCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static DALCategory ToDALCategory(this BLLCategory category)
        {
            if (category == null) return null;

            return new DALCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}