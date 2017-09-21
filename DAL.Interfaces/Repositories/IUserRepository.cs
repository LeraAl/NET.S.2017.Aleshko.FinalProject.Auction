using System.Collections.Generic;
using DAL.Interfaces.DTO;

namespace DAL.Interfaces.Repositories
{
    public interface IUserRepository: IRepository<DALUser>
    {
        IEnumerable<DALUser> GetByRoleId(int id);
    }
}