using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    class LotService: ILotSevice
    {
        private readonly IUnitOfWork _uow;
        private readonly ILotRepository _lotRepository;
        private readonly IRateRepository _rateRepository;

        public LotService(IUnitOfWork uow, ILotRepository lotRepository, IRateRepository rateRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _lotRepository = lotRepository ?? throw new ArgumentNullException(nameof(lotRepository));
            _rateRepository = rateRepository ?? throw new ArgumentNullException(nameof(rateRepository));
        }

        public IEnumerable<BLLLot> GetAll()
        {
            return _lotRepository.GetAll().Select(l => l.ToBLLLot());
        }

        public IEnumerable<BLLLot> GetAllExcept(int userId)
        {
            return _lotRepository.GetAll().Where(l => l.OwnerId != userId).Select(l => l.ToBLLLot());
        }

        public IEnumerable<BLLLot> GetUserLots(int userId)
        {
            return _lotRepository.GetAll().Where(l => l.OwnerId == userId).Select(l => l.ToBLLLot());
        }

        public IEnumerable<BLLLot> GetByCategory(int categoryId)
        {
            return _lotRepository.GetAll().Where(l => l.CategoryId == categoryId).Select(l => l.ToBLLLot());
        }

        public IEnumerable<string> GetLotNames()
        {
            return _lotRepository.GetAll().Select(l => l.Name);
        }

        public void AddRate(int lotId, BLLRate rate)
        {
            _lotRepository.AddRate(lotId, rate.ToDALRate());
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
            _lotRepository.Update(lot.ToDALLot());
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
