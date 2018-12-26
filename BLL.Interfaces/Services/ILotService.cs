using System.Collections.Generic;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Services
{
    public interface ILotService
    {
        BLLLot GetById(int id);
        IEnumerable<BLLLot> GetAll();
        IEnumerable<BLLLot> GetAllExcept(int userId);
        IEnumerable<BLLLot> GetUserLots(int userId);
        IEnumerable<BLLLot> GetByCategory(int categoryId);
        IEnumerable<BLLLot> GetLotByRegex(string regex);
        IEnumerable<BLLLot> GetByState(string state);
	    IEnumerable<BLLLot> GetFavorites(int userId);

        int GetStateId(string name);
        string GetLotStateName(int stateId);

        void AddRate(int lotId, BLLRate rate);
	    void AddToFavorites(int lotId, int userId);
	    void RemoveFromFavorites(int lotId, int userId);


        void Create(BLLLot lot);
        bool CanUserUpdate(int id);
        void Update(BLLLot lot);
        void UpdateState(BLLLot lot);
        bool CanUserDelete(int id);
        void Delete(BLLLot lot);
    }
}