using BLL.Interfaces.BLLEntities;
using MVC.Models;

namespace MVC.Infrasrtucture.Mappers
{
    public static class RateMappers
    {
        public static RateViewModel ToRateVM(this BLLRate model)
        {
            return new RateViewModel()
            {
                LotId = model.LotId,
                UserId = model.UserId,
                Datetime = model.Datetime,
                LotName = model.LotName,
                UserName = model.UserName,
                RateSize = model.RateSize
            };
        }
    }
}