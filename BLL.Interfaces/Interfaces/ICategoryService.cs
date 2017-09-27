using System.Collections.Generic;
using System.Runtime.InteropServices;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<BLLCategory> GetAll();
        BLLCategory GetById(int id);
        void Create(BLLCategory category);
        void UpdateName(int id, string newName);
        bool CanDelete(int id);
        void Delete(int id);
    }
}