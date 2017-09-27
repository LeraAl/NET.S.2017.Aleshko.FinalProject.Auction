using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{ 
    public interface IRoleRepository: IRepository<DALRole>
    {
        DALRole GetByName(string name);
        void AddRoleToUser(DALUser user, DALRole role);
        void DeleteRoleFromUser(DALUser user, DALRole role);
    }
}