using System.Collections.Generic;
using System.Runtime.CompilerServices;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Interfaces
{
    public interface IUserService
    {
        IEnumerable<BLLUser> GetAll();
        IEnumerable<BLLUser> GetAllByRole(string roleName);
        BLLUser GetById(int id);
        BLLUser GetByLogin(string login);

        void Create(BLLUser user);
        void Delete(int id);
        void UpdatePassword(int id, string newPassword);
        void UpdateFirstAndLastNames(int id, string firstName, string lastName);
    }
}