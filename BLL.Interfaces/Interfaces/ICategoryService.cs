using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface ICategoryService
    {
        void Create(BLLCategory category);
        void UpdateName(int id, string newName);
        bool CanDelete(int id);
        void Delete(int id);
    }
}