using MoviesAndMagic.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAndMagic.Data.Infrastructure
{
    //public class AuthRepository<T> : IAuthRepository<T> where T : class
    //{
    //    protected readonly IUnitOfWork unitOfWork;
    //    private MoviesAndMagicEntities moviesAndMagicEntities;
    //    private readonly IDbSet<T> dbSet;

    //    public AuthRepository(IUnitOfWork unitOfWork)
    //    {
    //        this.unitOfWork = unitOfWork;
    //    }


    //    // CRUD
    //    public virtual void Add(T entity)
    //    {
    //        dbSet.Add(entity);
    //    }

    //    public virtual void Update(T entity)
    //    {
    //        dbSet.Attach(entity);
    //        moviesAndMagicEntities.Entry(entity).State = EntityState.Modified;
    //    }

    //    public virtual void Delete(T entity)
    //    {
    //        dbSet.Remove(entity);
    //    }

    //    public virtual T GetById(int id)
    //    {
    //        return dbSet.Find(id);
    //    }

    //    public virtual IEnumerable<T> GetAll()
    //    {
    //        return dbSet.ToList();
    //    }

    //    public virtual int Count()
    //    {
    //        return dbSet.Count();
    //    }

    //    public T FindById<TId>(TId id)
    //    {
    //        return dbSet.Find(id);
    //    }

    //    private DbContext CurrentContext()
    //    {
    //        if (unitOfWork.GetContext() == null)
    //        {
    //            unitOfWork.SetContext(new AuthContext());
    //        }

    //        return unitOfWork.GetContext();
    //    }

    //    public void Add(User user)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
