using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public class CinemaService : ICinemaService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.Cinema> cinemaRepository;
        private readonly IUnitOfWork unitOfWork;

        public CinemaService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.cinemaRepository = new Repository<Domain.Cinema>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddCinema(Cinema value)
        {
            cinemaRepository.Add(value);

            //transform the object
            unitOfWork.Commit();
        }

        public bool ExistsCinema(string name)
        {
            var exists = cinemaRepository.GetAll().Any(c => c.CinemaCity == name);

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

