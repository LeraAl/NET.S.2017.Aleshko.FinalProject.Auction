using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface IRateRepository: IRepository<DALRate>
    {
        IEnumerable<DALRate> GetByUserId(int id);
        IEnumerable<DALRate> GetByLotId(int id);
        DALRate GetLotLastRate(int lotId);
    }
}