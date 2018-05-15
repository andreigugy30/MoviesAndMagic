using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public class GenreService : IGenreService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.Film> filmRepository;
        private IRepository<Domain.Genre> genreRepository;
        private readonly IUnitOfWork unitOfWork;
        

        public GenreService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.filmRepository = new Repository<Domain.Film>(factory);
            this.genreRepository = new Repository<Domain.Genre>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddGenre(Genre value)
        {
            genreRepository.Add(value);

            //transform the object
            unitOfWork.Commit();
        }

        public bool ExistsGenre(string name)
        {
            var exists = genreRepository.GetAll().Any(g => g.Name.ToLower() == name.ToLower());

            if (!exists)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
