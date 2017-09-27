using System.Collections.Generic;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface ILotService
    {
        // ToAsk GetAllExcept(int userId);???
        IEnumerable<BLLLot> GetAll();
        IEnumerable<BLLLot> GetAllExcept(int userId);
        IEnumerable<BLLLot> GetUserLots(int userId);
        IEnumerable<BLLLot> GetByCategory(int categoryId);
        IEnumerable<string> GetLotNames();

        void AddRate(int lotId, BLLRate rate);

        void Create(BLLLot lot);
        bool CanUserUpdate(int id);
        void Update(BLLLot lot);
        bool CanUserDelete(int id);
        void Delete(BLLLot lot);
    }
}