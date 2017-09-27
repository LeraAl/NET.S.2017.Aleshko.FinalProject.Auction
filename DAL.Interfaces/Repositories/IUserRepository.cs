using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRepository: IRepository<DALUser>
    {
        DALUser GetByLogin(string login);
        IEnumerable<DALUser> GetByRoleId(int id);
    }
}