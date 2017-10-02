using System.Collections.Generic;
using BLL.Interfaces.BLLEntities;

namespace BLL.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<BLLUser> GetAll();
        IEnumerable<BLLUser> GetAllByRole(int roleId);
        BLLUser GetById(int id);
        BLLUser GetByLogin(string login);

        void AddRoleToUser(int userId, BLLRole role);
        void DeleteRoleFromUser(int userId, BLLRole role);
        BLLRole GetRoleByName(string role);
        IEnumerable<BLLRole> GetUserRoles(int userId);
        IEnumerable<BLLRole> GetAllRoles();

        void Create(BLLUser user);
        void Delete(BLLUser user);
        void UpdatePassword(int id, string newPassword);
        void Update(BLLUser user);
    }
}