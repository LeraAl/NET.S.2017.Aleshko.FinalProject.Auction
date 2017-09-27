using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class RateMappers
    {
        public static DALRate ToDALRate(this Rate rate)
        {
            if (rate == null) return null;

            return new DALRate()
            {
                Id = rate.Id,
                Datetime = rate.Datetime,
                RateSize = rate.RateSize,
                LotId = rate.LotId,
                UserId = rate.UserId
            };
        }

        public static Rate ToRate(this DALRate rate)
        {
            if (rate == null) return null;

            return new Rate()
            {
                Id = rate.Id,
                Datetime = rate.Datetime,
                RateSize = rate.RateSize,
                LotId = rate.LotId,
                UserId = rate.UserId
            };
        }
    }
}