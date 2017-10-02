using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repositories;
using DAL.Mappers;
using ORM;

namespace DAL.Repositories
{
    public class RoleRepository: IRoleRepository
    {
        private readonly DbContext _context;

        public RoleRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<DALRole> GetAll()
        {
            return _context.Set<Role>()
                .ToList()
                .Select(r => r.ToDALRole());
        }

        public DALRole GetById(int id)
        {
            return _context.Set<Role>()
                .FirstOrDefault(r => r.Id == id)
                .ToDALRole();
        }

        public void Create(DALRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            _context.Set<Role>().Add(role.ToRole());
        }

        public void Update(DALRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));

            Role entity = _context.Set<Role>().FirstOrDefault(r => r.Id == role.Id);
            if (entity != null)
            {
                entity.Name = role.Name;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(DALRole role)
        {               
            if (role == null) throw new ArgumentNullException(nameof(role));

            Role entity = _context.Set<Role>().FirstOrDefault(r => r.Id == role.Id);
            if (entity != null)
                _context.Set<Role>().Remove(entity);
        }

        public DALRole GetByName(string name)
        {
            return _context.Set<Role>()
                .FirstOrDefault(r => name.Equals(r.Name, StringComparison.InvariantCultureIgnoreCase))
                .ToDALRole();
        }

        public IEnumerable<DALRole> GetUserRoles(int userId)
        {
            return _context.Set<User>()
                .FirstOrDefault(u => u.Id == userId)
                ?.Roles
                .ToList()
                .Select(r => r.ToDALRole());
        }

        public void AddRoleToUser(DALUser user, DALRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userEntity = _context.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            var roleEntity = _context.Set<Role>().FirstOrDefault(r => r.Id == role.Id);

            userEntity?.Roles.Add(roleEntity);
        }

        public void DeleteRoleFromUser(DALUser user, DALRole role)
        {
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (user == null) throw new ArgumentNullException(nameof(user));

            var userEntity = _context.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            var roleEntity = _context.Set<Role>().FirstOrDefault(r => r.Id == role.Id);

            userEntity?.Roles.Remove(roleEntity);
        }
    }
}