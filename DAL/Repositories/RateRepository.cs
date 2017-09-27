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
    public class RateRepository : IRateRepository
    {
        private readonly DbContext _context;

        public RateRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DALRate> GetAll()
        {
            return _context.Set<Rate>()
                .Select(r => r.ToDALRate());
        }

        public DALRate GetById(int id)
        {
            return _context.Set<Rate>()
                .FirstOrDefault(r => r.Id == id)
                .ToDALRate();
        }

        public IEnumerable<DALRate> GetByUserId(int id)
        {
            return _context.Set<Rate>()
                .Where(r => r.UserId == id)
                .Select(r => r.ToDALRate());
        }

        public IEnumerable<DALRate> GetByLotId(int id)
        {
            return _context.Set<Rate>()
                .Where(r => r.LotId == id)
                .Select(r => r.ToDALRate());
        }

        public DALRate GetLotLastRate(int lotId)
        {
            return _context.Set<Rate>()
                .Where(r => r.LotId == lotId)
                .OrderBy(r => r.Datetime)
                .FirstOrDefault().ToDALRate();
        }

        public void Create(DALRate rate)
        {
            if (rate == null) throw new ArgumentNullException(nameof(rate));

            _context.Set<Rate>().Add(rate.ToRate());
        }

        //ToAsk Можно ли оставить NotImplementedException, если  этого действия не должно быть?
        public void Update(DALRate rate)
        {
            if (rate == null) throw new ArgumentNullException(nameof(rate));

            Rate entity = _context.Set<Rate>().FirstOrDefault(r => r.Id == rate.Id);
            if (entity != null)
            {
                entity.LotId = rate.LotId;
                entity.UserId = rate.UserId;
                entity.Datetime = rate.Datetime;
                entity.RateSize = rate.RateSize;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(DALRate rate)
        {
            if (rate == null) throw new ArgumentNullException(nameof(rate));

            Rate entity = _context.Set<Rate>().FirstOrDefault(r => r.Id == rate.Id);
            if (entity != null)
                _context.Set<Rate>().Remove(entity);
        }
    }
}
