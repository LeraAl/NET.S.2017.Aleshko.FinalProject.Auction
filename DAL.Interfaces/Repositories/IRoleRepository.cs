using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface IRoleRepository: IRepository<DALRole>
    {
        DALRole GetByName(string name);
        void AddRoleToUser(int roleId, int userId);
        void DeleteRoleFromUser(int roleId, int userId);
    }
}