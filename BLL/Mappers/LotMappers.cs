using System;
using System.Text;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repositories;

namespace BLL.Mappers
{
    public static class LotMappers
    {
        public static BLLLot ToBLLLot(this DALLot lot, ILotStateRepository lotStateRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            if (lot == null) return null;

            return new BLLLot()
            {
                Id = lot.Id,
                Name = lot.Name,
                CategoryId = lot.CategoryId,
                Description = lot.Description != null ? Encoding.ASCII.GetString(lot.Description) : null,
                OwnerId  = lot.OwnerId,
                Image = lot.Image,
                StartPrice = lot.StartPrice,
                CurrentPrice = lot.Price,
                StateId = lot.StateId,
                StartDatetime = lot.StartDatetime,
                Owner = userRepository.GetById(lot.OwnerId).ToBLLUser(),
                Category = categoryRepository.GetById(lot.CategoryId).ToBLLCategory(),
                State = lotStateRepository.GetById(lot.StateId).Name
            };
        }

        public static DALLot ToDALLot(this BLLLot lot)
        {
            if (lot == null) return null;

            return new DALLot()
            {
                Id = lot.Id,
                Name = lot.Name,
                CategoryId = lot.CategoryId,
                Description = Encoding.ASCII.GetBytes(lot.Description),
                OwnerId = lot.OwnerId,
                Image = lot.Image,
                StartPrice = lot.StartPrice,
                Price = lot.CurrentPrice,
                StartDatetime = lot.StartDatetime,
                StateId = lot.StateId
            };
        }
    }
}