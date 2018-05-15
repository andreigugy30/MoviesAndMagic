using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Domain;
using System.Data.Entity;

namespace MoviesAndMagic.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private DbContext _dbContext;

        private MoviesAndMagicEntities moviesAndMagicEntities;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected MoviesAndMagicEntities DataContext
        {
            get { return moviesAndMagicEntities ?? (moviesAndMagicEntities = databaseFactory.Get()); }
        }

        public DbContext GetContext()
        {
            return _dbContext;
        }

        public void SetContext(DbContext defaultContext)
        {
            _dbContext = defaultContext;
        }

        public void Commit()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }
    }
}

