using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Services;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class LotService: ILotService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILotRepository _lotRepository;
        private readonly IRateRepository _rateRepository;
        private readonly ILotStateRepository _lotStateRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public LotService(IUnitOfWork uow, ILotRepository lotRepository, IRateRepository rateRepository, ILotStateRepository lotStateRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _lotRepository = lotRepository ?? throw new ArgumentNullException(nameof(lotRepository));
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
            _lotStateRepository = lotStateRepository ?? throw new ArgumentNullException(nameof(lotStateRepository));
        }


        public BLLLot GetById(int id)
        {
            return _lotRepository.GetById(id)
                .ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository);
        }

        public IEnumerable<BLLLot> GetAll()
        {
            return _lotRepository.GetAll()
                .Select(l => l.ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository));
        }

        public IEnumerable<BLLLot> GetAllExcept(int userId)
        {
            return _lotRepository.GetAll()
                .Where(l => l.OwnerId != userId)
                .Select(l => l.ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository));
        }

        public IEnumerable<BLLLot> GetUserLots(int userId)
        {
            return _lotRepository.GetAll()
                .Where(l => l.OwnerId == userId)
                .Select(l => l.ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository));
        }

        public IEnumerable<BLLLot> GetByCategory(int categoryId)
        {
            return _lotRepository.GetAll()
                .Where(l => l.CategoryId == categoryId)
                .Select(l => l.ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository));
        }

        public IEnumerable<BLLLot> GetLotByRegex(string regex)
        {
            return _lotRepository.GetAll()
                .Where(l => l.Name.IndexOf(regex, StringComparison.InvariantCultureIgnoreCase) != -1)
                .Select(l => l.ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository));
        }

        public IEnumerable<BLLLot> GetByState(string state)
        {
            int stateId = GetStateId(state);
            return _lotRepository.GetAll()
                .Where(l => l.StateId == stateId)
                .Select(l => l.ToBLLLot(_lotStateRepository, _userRepository, _categoryRepository));
        }

        public int GetStateId(string stateName)
        {
            return _lotStateRepository.GetByName(stateName).Id;
        }

        public string GetLotStateName(int stateId)
        {
            return _lotStateRepository.GetById(stateId).Name;
        }

        public void AddRate(int lotId, BLLRate rate)
        {
            var lot = _lotRepository.GetById(lotId);
            lot.Price += rate.RateSize;
            
            _rateRepository.Create(rate.ToDALRate());
            _lotRepository.Update(lot);

            _uow.Commit();
        }

        public void Create(BLLLot lot)
        {
            _lotRepository.Create(lot.ToDALLot());
            _uow.Commit();
        }

        public bool CanUserUpdate(int id)
        {
            return !_rateRepository.GetByLotId(id).Any();
        }

        public void Update(BLLLot lot)
        {
            if(_rateRepository.GetByLotId(lot.Id).Any())
                throw new InvalidOperationException("Lot cannot be updated when it has rates.");

            var newLot = GetById(lot.Id);

            newLot.Name = lot.Name;
            newLot.Description = lot.Description;
            newLot.Image = lot.Image ?? newLot.Image;
            newLot.CategoryId = lot.CategoryId;
            newLot.StartPrice = lot.StartPrice;
            newLot.CurrentPrice = lot.StartPrice;

            _lotRepository.Update(newLot.ToDALLot());
            _uow.Commit();
        }

        public void UpdateState(BLLLot lot)
        {
            var newLot = GetById(lot.Id);

            newLot.StateId = lot.StateId;

            _lotRepository.Update(newLot.ToDALLot());
            _uow.Commit();
        }

        public bool CanUserDelete(int id)
        {
            return !_rateRepository.GetByLotId(id).Any();
        }

        public void Delete(BLLLot lot)
        {
            _lotRepository.Delete(lot.ToDALLot());
            _uow.Commit();
        }
    }
}
