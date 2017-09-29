using BLL.Interfaces.BLLEntities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class RateMappers
    {
        public static BLLRate ToBLLRate(this DALRate rate)
        {
            if (rate == null) return null;

            return new BLLRate()
            {
                Id = rate.Id,
                Datetime = rate.Datetime,
                LotId = rate.LotId,
                RateSize = rate.RateSize,
                UserId = rate.UserId
            };
        }

        public static DALRate ToDALRate(this BLLRate rate)
        {
            if (rate == null) return null;

            return new DALRate()
            {
                Id = rate.Id,
                Datetime = rate.Datetime,
                LotId = rate.LotId,
                RateSize = rate.RateSize,
                UserId = rate.UserId
            };
        }
    }
}