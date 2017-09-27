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
    public class LotStateRepository: ILotStateRepository
    {
        private readonly DbContext _context;

        public LotStateRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DALLotState> GetAll()
        {
            return _context.Set<LotState>().Select(s => s.ToDALLotState());
        }

        public DALLotState GetById(int id)
        {
            return _context.Set<LotState>().FirstOrDefault(s => s.Id == id).ToDALLotState();
        }

        public DALLotState GetByName(string name)
        {
            return _context.Set<LotState>()
                .FirstOrDefault(s => String.Equals(name, s.Name, StringComparison.InvariantCultureIgnoreCase))
                .ToDALLotState();
        }

        public void Create(DALLotState state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            _context.Set<LotState>().Add(state.ToLotState());
        }

        public void Update(DALLotState state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            LotState entity = _context.Set<LotState>().FirstOrDefault(s => s.Id == state.Id);
            if (entity != null)
            {
                entity.Name = state.Name;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(DALLotState state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));

            LotState entity = _context.Set<LotState>().FirstOrDefault(s => s.Id == state.Id);
            if (entity != null)
                _context.Set<LotState>().Remove(entity);
        }

    }
}