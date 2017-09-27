using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface IUserService
    {
        IEnumerable<BLLUser> GetAll();
        IEnumerable<BLLUser> GetAllByRole(int roleId);
        BLLUser GetById(int id);
        BLLUser GetByLogin(string login);
        IEnumerable<BLLRole> GetAllRoles();

        void Create(BLLUser user);
        void Delete(BLLUser user);
        void UpdatePassword(int id, string newPassword);
        void Update(BLLUser user);
    }
}