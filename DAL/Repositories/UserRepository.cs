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
    public class UserRepository: IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }
        
        public IEnumerable<DALUser> GetAll()
        {
            return _context.Set<User>()
                .ToList()
                .Select(u => u.ToDALUser());
        }

        public DALUser GetById(int id)
        {
            return _context.Set<User>()
                .FirstOrDefault(u => u.Id == id)
                .ToDALUser();
        }

        public void Create(DALUser entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _context.Set<User>().Add(entity.ToORMUser());
        }

        public void Update(DALUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            User entity = _context.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            if (entity != null)
            {
                entity.Login = user.Login;
                entity.FirstName = user.FirstName;
                entity.LastName = user.LastName;
                entity.Email = user.Email;
                entity.Password = user.Password;

                _context.Entry(entity).State = EntityState.Modified;
            }
        }

        public void Delete(DALUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            User entity = _context.Set<User>().FirstOrDefault(u => u.Id == user.Id);
            if (entity != null)
                _context.Set<User>().Remove(entity);
        }

        public DALUser GetByLogin(string login)
        {
            var users = _context.Set<User>();
            var user = users.FirstOrDefault(u =>
                String.Equals(login, u.Login));

            return user.ToDALUser();
        }

        public IEnumerable<DALUser> GetByRoleId(int id)
        {
            return _context.Set<User>()
                .Where(u => u.Roles.Any(r => r.Id == id))
                .ToList()
                .Select(u => u.ToDALUser());
        }
    }
}