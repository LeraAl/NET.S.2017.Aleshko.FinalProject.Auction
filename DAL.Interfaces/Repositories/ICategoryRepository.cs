using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface ICategoryRepository: IRepository<DALCategory>
    {
        DALCategory GetByName(string name);
    }
}