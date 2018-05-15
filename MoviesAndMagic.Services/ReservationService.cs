using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAndMagic.Services
{
    public class ReservationService : IReservationService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.Seat> seatRepository;
        private IRepository<Domain.Show> showRepository;
        private IRepository<Domain.Reservation> reservationRepository;
        private readonly IUnitOfWork unitOfWork;

        public ReservationService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.seatRepository = new Repository<Domain.Seat>(factory);
            this.showRepository = new Repository<Domain.Show>(factory);
            this.reservationRepository = new Repository<Domain.Reservation>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddReservation(Reservation value)
        {
            reservationRepository.Add(value);

            //transform the object
            unitOfWork.Commit();
       
        }

        public void UpdateReservation(Reservation value)
        {
            reservationRepository.Update(value);
            unitOfWork.Commit();
        }

        public bool ExistsReservation(int showId, int seatId)
        {
            var exists = reservationRepository.GetAll().Any(f => f.Show.Id == showId && f.Seat.Id == seatId);

            if (!exists)
            {
                return false;
            }

            return true;
        }

        public bool IsUsedReservation(int id)
        {
            var used = reservationRepository.GetAll().Any(f => f.SeatId == id);

            if (!used)
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
