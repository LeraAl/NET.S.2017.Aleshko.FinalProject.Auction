using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface ILotRepository: IRepository<DALLot>
    {
        IEnumerable<DALLot> GetByUserId(int id);
        IEnumerable<DALLot> GetByName(string name);
        IEnumerable<DALLot> GetByCategoryId(int id);
        IEnumerable<DALLot> GetByMinPrice(decimal minPrice);
        IEnumerable<DALLot> GetByMaxPrice(decimal maxPrice);
    }
}