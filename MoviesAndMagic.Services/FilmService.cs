using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public class FilmService : IFilmService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.Film> filmRepository;
        private IRepository<Domain.Genre> genreRepository;
        private IRepository<Domain.Show> showRepository;
        private readonly IUnitOfWork unitOfWork;

        public FilmService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.filmRepository = new Repository<Domain.Film>(factory);
            this.genreRepository = new Repository<Domain.Genre>(factory);
            this.showRepository = new Repository<Domain.Show>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddFilm(Film value)
        {
            filmRepository.Add(value);

            //transform the object
            unitOfWork.Commit();
        }

        public void UpdateFilm(Film value)
        {
            filmRepository.Update(value);
            unitOfWork.Commit();
        }

        public void DeleteFilm(Film value)
        {
            filmRepository.Delete(value);
            unitOfWork.Commit();
        }

        public bool ExistsFilm(string name)
        {
            var exists = filmRepository.GetAll().ToList().FirstOrDefault(f => f.Name.ToLower() == name.ToLower());

            if (exists != null)
            {
                return true;
            }

            return false;

        }

        public bool IsUsedFilm(int id)
        {
            var used = showRepository.GetAll().ToList().FirstOrDefault(f => f.FilmId == id);

            if (used != null)
            {
                return true;
            }

            return false;

        }
    }
}





