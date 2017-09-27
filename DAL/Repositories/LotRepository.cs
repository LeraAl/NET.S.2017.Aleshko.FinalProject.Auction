using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Repositories
{
    public class LotRepository : ILotRepository
    {
        private readonly DbContext _context;

        public LotRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DALLot> GetAll()
        {
            return _context.Set<Lot>().Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByCategoryId(int id)
        {
            return _context.Set<Lot>().Where(l => l.CategoryId == id).Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByLotStateId(int id)
        {
            return _context.Set<Lot>().Where(l => l.StateId == id).Select(l => l.ToDALLot());
        }

        public DALLot GetById(int id)
        {
            return _context.Set<Lot>().FirstOrDefault(l => l.Id == id).ToDALLot();
        }

        public IEnumerable<DALLot> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _context.Set<Lot>()
                .Where(l => l.Price >= minPrice && l.Price <= maxPrice)
                .Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByName(string name)
        {
            return _context.Set<Lot>()
                .Where(l => String.Equals(name, l.Name, StringComparison.InvariantCultureIgnoreCase))
                .Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByUserId(int id)
        {
            return _context.Set<Lot>().Where(l => l.OwnerId == id).Select(l => l.ToDALLot());
        }

        public void Create(DALLot lot)
        {
            if (lot == null) throw new ArgumentNullException(nameof(lot));

            _context.Set<Lot>().Add(lot.ToLot());
        }

        //ToAsk CASCADE DELETE???
        public void Delete(DALLot lot)
        {
            if (lot == null) throw new ArgumentNullException(nameof(lot));

            Lot entity = _context.Set<Lot>().FirstOrDefault(l => l.Id == lot.Id);
            if (entity != null)
                _context.Set<Lot>().Remove(entity);
        }

        public void Update(DALLot lot)
        {
            if (lot == null) throw new ArgumentNullException(nameof(lot));

            Lot entity = _context.Set<Lot>().FirstOrDefault(l => l.Id == lot.Id);
            if (entity != null)
            {
                lot.Name = lot.Name;
                lot.Price = lot.Price;
                lot.StartPrice = lot.StartPrice;
                lot.StartDatetime = lot.StartDatetime;
                lot.CategoryId = lot.CategoryId;
                lot.Description = lot.Description;
                lot.Image = lot.Image;
                lot.StateId = lot.StateId;
                lot.OwnerId = lot.OwnerId;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}