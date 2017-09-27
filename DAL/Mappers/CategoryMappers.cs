using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class CategoryMappers
    {
        public static Category ToCategory(this DALCategory category)
        {
            if (category == null) return null;

            return new Category()
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public static DALCategory ToDALCategory(this Category category)
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