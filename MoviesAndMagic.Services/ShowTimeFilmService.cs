using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAndMagic.Services
{
    public class ShowTimeFilmService : IShowTimeFilmService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.ShowTimeFilm> showTimeFilmRepository;
        private readonly IUnitOfWork unitOfWork;

       

        public ShowTimeFilmService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.showTimeFilmRepository = new Repository<Domain.ShowTimeFilm>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddShowTimeFilm(ShowTimeFilm value)
        {
            showTimeFilmRepository.Add(value);

            //transform the object
            unitOfWork.Commit();
        }

        public bool ExistsStf(DateTime ShowTime)
        {
            var exists = showTimeFilmRepository.GetAll().Any(s => s.ShowTime == ShowTime);
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
