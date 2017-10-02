using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using DAL.Interfaces.Repositories;
using DAL.Logger;

namespace DAL.Repositories
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ILogger _logger = LoggerAdapter.GetLogger();
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            //ToAsk Is that checking must be here?
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }

            catch (DbEntityValidationException ex)
            {
                foreach (var item in ex.EntityValidationErrors)
                {
                    var entry = item.Entry;
                    var entityTypeName = entry.Entity.GetType().Name;

                    foreach (var subItem in item.ValidationErrors)
                    {
                        var message =
                            $"Error '{subItem.ErrorMessage}' occurred in {entityTypeName} at {subItem.PropertyName}";
                        _logger.Error(message);
                    }

                    // Roll back
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues.SetValues(entry.OriginalValues);
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Unchanged;
                            break;
                    }

                    throw;
                }

            }
            catch (SqlException ex)
            {
                _logger.Error(ex.Message);
                throw;
            }
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}