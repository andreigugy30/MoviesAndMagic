using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public class ShowService : IShowService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.Show> showRepository;
        private IRepository<Domain.Cinema> cinemaRepository;
        private IRepository<Domain.Film> filmRepository;
        private IRepository<Domain.ShowTimeFilm> showtimefilmRepository;
        private readonly IUnitOfWork unitOfWork;


        public ShowService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.showRepository = new Repository<Domain.Show>(factory);
            this.cinemaRepository = new Repository<Domain.Cinema>(factory);
            this.filmRepository = new Repository<Domain.Film>(factory);
            this.showtimefilmRepository = new Repository<Domain.ShowTimeFilm>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddShow(Show value)
        {
            showRepository.Add(value);

            //transform the object
            unitOfWork.Commit();
        }

        public bool ExistsShow(string name)// la fiecare cate ceva si verificate dupa id
        {
            var exists = showRepository.GetAll().Any(c => c.Cinema.CinemaCity == name 
                                                          && c.Film.Name == name && c.ShowTimeFilm.ShowTime.ToString("dd/MM/yyyy hh:mm") == name);

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
