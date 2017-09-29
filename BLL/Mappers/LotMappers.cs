﻿using System;
using System.Text;
using BLL.Interfaces.BLLEntities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class LotMappers
    {
        public static BLLLot ToBLLLot(this DALLot lot)
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
                StateId = lot.StateId
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
                Price = lot.CurrentPrice
                //ToASK как получить имя категории юзера и т.д.
            };
        }
    }
}