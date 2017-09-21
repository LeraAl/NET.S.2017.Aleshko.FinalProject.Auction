namespace DAL.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}