using System;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Mappers
{
    public static class LotMappers
    {
        public static Lot ToLot(this DALLot lot)
        {
            if (lot == null) return null;
            
            return new Lot()
            {
                Id =  lot.Id,
                Name = lot.Name,
                Price = lot.Price,
                StartPrice = lot.StartPrice,
                StartDatetime = lot.StartDatetime,
                CategoryId = lot.CategoryId,
                Description = lot.Description,
                Image = lot.Image,
                StateId = lot.StateId,
                OwnerId = lot.OwnerId
            };
        }

        public static DALLot ToDALLot(this Lot lot)
        {
            if (lot == null) return null;

            return new DALLot()
            {
                Id = lot.Id,
                Name = lot.Name,
                Price = lot.Price,
                StartPrice = lot.StartPrice,
                StartDatetime = lot.StartDatetime,
                CategoryId = lot.CategoryId,
                Description = lot.Description,
                Image = lot.Image,
                StateId = lot.StateId,
                OwnerId = lot.OwnerId
            };
        }
    }
}