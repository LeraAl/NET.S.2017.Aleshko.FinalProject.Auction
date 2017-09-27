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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DbContext _context;

        public CategoryRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DALCategory> GetAll()
        {
            return _context.Set<Category>().Select(c => c.ToDALCategory());
        }

        public DALCategory GetById(int id)
        {
            return _context.Set<Category>().FirstOrDefault(c => c.Id == id).ToDALCategory();
        }

        public DALCategory GetByName(string name)
        {
            return _context.Set<Category>()
                .FirstOrDefault(c => String.Equals(name, c.Name, StringComparison.InvariantCultureIgnoreCase))
                .ToDALCategory();
        }

        public void Create(DALCategory category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            _context.Set<Category>().Add(category.ToCategory());
        }

        public void Update(DALCategory category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            Category entity = _context.Set<Category>().FirstOrDefault(c => c.Id == category.Id);
            if (entity != null)
            {
                entity.Name = category.Name;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(DALCategory category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));

            Category entity = _context.Set<Category>().FirstOrDefault(c => c.Id == category.Id);
            if (entity != null)
                _context.Set<Category>().Remove(entity);
        }
    }
}