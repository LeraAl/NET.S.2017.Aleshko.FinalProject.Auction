using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces.BLLEntities;
using BLL.Interfaces.Interfaces;
using BLL.Mappers;
using DAL.Interfaces.Repositories;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUnitOfWork uow, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        }

        public IEnumerable<BLLUser> GetAll()
        {
            return _userRepository.GetAll().Select(u => u.ToBLLUser());
        }

        public IEnumerable<BLLUser> GetAllByRole(int roleId)
        {
            return _userRepository.GetByRoleId(roleId).Select(u => u.ToBLLUser());
        }

        public BLLUser GetById(int id)
        {
            return _userRepository.GetById(id).ToBLLUser();
        }

        public BLLUser GetByLogin(string login)
        {
            return _userRepository.GetByLogin(login).ToBLLUser();
        }

        public void AddRoleToUser(int userId, BLLRole role)
        {
            _roleRepository.AddRoleToUser(GetById(userId).ToDALUser(), role.ToDALRole());
        }

        public IEnumerable<BLLRole> GetUserRoles(int userId)
        {
            return _roleRepository.GetUserRoles(userId).Select(r => r.ToBLLRole());
        }

        public IEnumerable<BLLRole> GetAllRoles()
        {
            return _roleRepository.GetAll().Select(r => r.ToBLLRole());
        }

        public void Create(BLLUser user)
        {
            _userRepository.Create(user.ToDALUser());
            _uow.Commit();
        }

        public void Delete(BLLUser user)
        {
            _userRepository.Delete(user.ToDALUser());
        }

        public void UpdatePassword(int id, string newPassword)
        {
            var user = _userRepository.GetById(id);
            user.Password = newPassword;
            _userRepository.Update(user);
            _uow.Commit();
        }

        public void Update(BLLUser user)
        {
            _userRepository.Update(user.ToDALUser());
            _uow.Commit();
        }
    }
}