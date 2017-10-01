using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class RateService: IRateService
    {
        private readonly IRateRepository _rateRepository;
        private readonly ILotRepository _lotRepository;
        private readonly IUserRepository _userRepository;

        public RateService(IRateRepository rateRepository, ILotRepository lotRepository, IUserRepository userRepository)
        {
            _lotRepository = lotRepository ?? throw new ArgumentNullException(nameof(lotRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        public IEnumerable<BLLRate> GetUserRates(int userId)
        {
            return _rateRepository.GetByUserId(userId)
                .Select(r => r.ToBLLRate(_userRepository, _lotRepository));
        }

        public IEnumerable<BLLRate> GetLotRates(int lotId)
        {
            return _rateRepository.GetByLotId(lotId)
                .Select(r => r.ToBLLRate(_userRepository, _lotRepository));
        }

        public BLLRate GetLotLastRate(int lotId)
        {
            return _rateRepository.GetLotLastRate(lotId).ToBLLRate(_userRepository, _lotRepository);
        }
    }
}