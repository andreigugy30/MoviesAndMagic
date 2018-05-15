using System.Data.Entity;

namespace MoviesAndMagic.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        DbContext GetContext();
        void SetContext(DbContext defaultContext);
    }
}