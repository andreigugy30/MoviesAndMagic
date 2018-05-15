using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface IReservationService
    {
        void AddReservation(Reservation value);
        void UpdateReservation(Reservation value);
        bool ExistsReservation(int showId, int seatId);
        bool IsUsedReservation(int id);
    }
}