using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repositories;

namespace BLL.Mappers
{
    public static class RateMappers
    {
        public static BLLRate ToBLLRate(this DALRate rate, IUserRepository userRepository, ILotRepository lotRepository)
        {
            if (rate == null) return null;

            return new BLLRate()
            {
                Id = rate.Id,
                Datetime = rate.Datetime,
                LotId = rate.LotId,
                RateSize = rate.RateSize,
                UserId = rate.UserId,
                LotName = lotRepository.GetById(rate.LotId).Name,
                UserName = userRepository.GetById(rate.UserId).Login

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