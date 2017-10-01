using System;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using MVC.Models.Lot;

namespace MVC.Infrasrtucture.Mappers
{
     public static class LotMappers
     {
        public static LotViewModel ToLotVM(this BLLLot lot)
        {
            var lotVM = new LotViewModel
            {
                Id = lot.Id,
                Name = lot.Name,
                CurrentPrice = lot.CurrentPrice,
                StartPrice = lot.StartPrice,
                StartDatetime = lot.StartDatetime,
                Description = lot.Description,
                Image = lot.Image,
                OwnerName = lot.Owner.Login,
                OwnerEmail = lot.Owner.Email,
                Category = lot.Category.Name,
                State = lot.State
            };
            return lotVM;
        }

         public static LotShortViewModel ToLotShortVM(this BLLLot lot)
         {
             var lotShortVM = new LotShortViewModel
             {
                 Id = lot.Id,
                 Name = lot.Name,
                 CurrentPrice = lot.CurrentPrice,
                 StartDatetime = lot.StartDatetime,
                 Description = lot.Description,
                 Image = lot.Image,
                 OwnerName = lot.Owner.Login
             };

             return lotShortVM;
         }

        public static BLLLot ToBLLLot(this LotCreateModel lot)
        {
            return new BLLLot()
            {
                Id = lot.Id,
                Name = lot.Name,
                StartPrice = lot.StartPrice,
                Description = lot.Description,
                Image = lot.Image,
                CategoryId = lot.CategoryId,
                CurrentPrice = lot.StartPrice,
                StartDatetime = DateTime.Now
            };
        }

         public static LotCreateModel ToLotCreateModel(this BLLLot lot)
         {
             return new LotCreateModel()
             {
                 Id = lot.Id,
                 StartPrice = lot.StartPrice, 
                 Name = lot.Name,
                 Description = lot.Description,
                 Image = lot.Image,
                 CategoryId = lot.CategoryId
             };
         }
    }
}