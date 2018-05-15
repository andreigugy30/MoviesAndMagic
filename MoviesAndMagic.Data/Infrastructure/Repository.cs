using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Data.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private MoviesAndMagicEntities moviesAndMagicEntities;
        private readonly IDbSet<T> dbSet;


        public Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbSet = MoviesAndMagicEntities.Set<T>();
        }


        public IDatabaseFactory DatabaseFactory { get; }

        protected MoviesAndMagicEntities MoviesAndMagicEntities
        {
            get { return moviesAndMagicEntities ?? (moviesAndMagicEntities = DatabaseFactory.Get()); }
        }
        
        // CRUD
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            moviesAndMagicEntities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual int Count()
        {
            return dbSet.Count();
        }

        public T FindById<TId>(TId id)
        {
            return dbSet.Find(id);
        }
    }
}
