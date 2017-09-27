using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Mappers;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILotRepository _lotRepository;

        public CategoryService(IUnitOfWork uow, ICategoryRepository categoryRepository, ILotRepository lotRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _lotRepository = lotRepository ?? throw new ArgumentNullException(nameof(lotRepository));
        }

        public IEnumerable<BLLCategory> GetAll()
        {
            return _categoryRepository.GetAll().Select(c => c.ToBLLCategory());
        }

        public BLLCategory GetById(int id)
        {
            return _categoryRepository.GetById(id).ToBLLCategory();
        }

        public void Create(BLLCategory category)
        {
            _categoryRepository.Create(category.ToDALCategory());
            _uow.Commit();
        }

        public void UpdateName(int id, string newName)
        {
            _categoryRepository.Update(new DALCategory(){Id = id, Name = newName});
            _uow.Commit();
        }

        public bool CanDelete(int id)
        {
            return !_lotRepository.GetByCategoryId(id).Any();
        }

        public void Delete(int id)
        {
            _categoryRepository.Delete(new DALCategory(){Id = id});
        }
    }
}