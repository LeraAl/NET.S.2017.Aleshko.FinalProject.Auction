using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class RateService: IRateService
    {
        private readonly IRateRepository _rateRepository;

        public RateService(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        public IEnumerable<BLLRate> GetUserRates(int userId)
        {
            return _rateRepository.GetByUserId(userId).Select(r => r.ToBLLRate());
        }

        public IEnumerable<BLLRate> GetLotRates(int lotId)
        {
            return _rateRepository.GetByLotId(lotId).Select(r => r.ToBLLRate());
        }

        public BLLRate GetLotLastRate(int lotId)
        {
            return _rateRepository.GetLotLastRate(lotId).ToBLLRate();
        }
    }
}