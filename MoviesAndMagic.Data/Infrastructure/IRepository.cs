using System.Collections.Generic;

namespace MoviesAndMagic.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        int Count();
        
        T FindById<TId>(TId id);
       
    }
}