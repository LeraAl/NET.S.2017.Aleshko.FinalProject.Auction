using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface ILotStateRepository : IRepository<DALLotState>
    {
        DALLotState GetByName(string name);
    }
}