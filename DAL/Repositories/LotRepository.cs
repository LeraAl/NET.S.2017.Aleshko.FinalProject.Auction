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
            return _context.Set<Lot>().ToList().Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByCategoryId(int id)
        {
            return _context.Set<Lot>().Where(l => l.CategoryId == id).ToList().Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByLotStateId(int id)
        {
            return _context.Set<Lot>().Where(l => l.StateId == id).ToList().Select(l => l.ToDALLot());
        }

        public DALLot GetById(int id)
        {
            return _context.Set<Lot>().FirstOrDefault(l => l.Id == id).ToDALLot();
        }

        public IEnumerable<DALLot> GetByPriceRange(decimal minPrice, decimal maxPrice)
        {
            return _context.Set<Lot>()
                .Where(l => l.Price >= minPrice && l.Price <= maxPrice)
                .ToList()
                .Select(l => l.ToDALLot());
        }

	    public IEnumerable<DALLot> GetFavouriteLots(int userId)
	    {
		    return _context.Set<Favorite>()
			    .Where(fav => fav.UserId == userId)
			    .Select(fav => fav.Lot)
			    .ToList()
			    .Select(l => l.ToDALLot());
	    }

	    public void AddToFavorites(int lotId, int userId)
	    {
		    Favorite favorite = new Favorite()
		    {
				LotId = lotId,
				UserId = userId,
		    };

		    _context.Set<Favorite>().Add(favorite);
	    }

	    public void RemoveFromFavorites(int lotId, int userId)
	    {
		    Favorite favorite = _context.Set<Favorite>()
			    .FirstOrDefault(fav => fav.UserId == userId && fav.LotId == lotId);

			if (favorite != null)
				_context.Set<Favorite>().Remove(favorite);
	    }

	    public IEnumerable<DALLot> GetByName(string name)
        {
            return _context.Set<Lot>()
                .Where(l => String.Equals(name, l.Name, StringComparison.InvariantCultureIgnoreCase))
                .ToList()
                .Select(l => l.ToDALLot());
        }

        public IEnumerable<DALLot> GetByUserId(int id)
        {
            return _context.Set<Lot>()
                .Where(l => l.OwnerId == id)
                .ToList()
                .Select(l => l.ToDALLot());
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
                entity.Name = lot.Name;
                entity.Price = lot.Price;
                entity.StartPrice = lot.StartPrice;
                entity.StartDatetime = lot.StartDatetime;
                entity.CategoryId = lot.CategoryId;
                entity.Description = lot.Description;
                entity.Image = lot.Image;
                entity.StateId = lot.StateId;
                entity.OwnerId = lot.OwnerId;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}