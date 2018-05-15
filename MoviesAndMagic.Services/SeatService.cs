using MoviesAndMagic.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Domain;
using System.Web.ModelBinding;

namespace MoviesAndMagic.Services
{
    //public interface ISeatService
    //{
    //    void AddSeat(Seat model);
    //}

    public class SeatService : ISeatService
    {
        private DatabaseFactory factory;
        private IRepository<Domain.Seat> seatRepository;
        private readonly IUnitOfWork unitOfWork;

        public SeatService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.seatRepository = new Repository<Domain.Seat>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }

        public void AddSeat(Domain.Seat value)
        {
            //var seatNoMax = 100;
            var seats = seatRepository.GetAll().Where(s => s.SeatNo == value.SeatNo
            && s.Status == value.Status).Any();
            if (!seats)   
            {
                seatRepository.Add(value);

                //transform the object
                unitOfWork.Commit();
            }else
            {
                throw new ArgumentException("This seat already exist and it is booked");
            }

        }

        public bool ExistsSeat(int seatNo)
        {
            var exists = seatRepository.GetAll().ToList().FirstOrDefault(f => f.SeatNo==seatNo );

            if (exists != null)
            {
                return true;
            }

            return false;
        }

        public void UpdateSeat(Seat seat)
        {
            seatRepository.Update(seat);
            unitOfWork.Commit();
        }

        public void DeleteSeat(Seat seat)
        {
            seatRepository.Delete(seat);
            unitOfWork.Commit();
        }

    }
}
