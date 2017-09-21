using System;
using System.Data.Entity;
using DAL.Interfaces.Repositories;

namespace DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //ToAsk Is that checking must be here?
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}