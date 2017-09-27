using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class LotStateMappers
    {
        public static LotState ToLotState(this DALLotState state)
        {
            if (state == null) return null;

            return new LotState()
            {
                Id = state.Id,
                Name = state.Name
            };
        }

        public static DALLotState ToDALLotState(this LotState state)
        {
            if (state == null) return null;

            return new DALLotState()
            {
                Id = state.Id,
                Name = state.Name
            };
        }
    }
}