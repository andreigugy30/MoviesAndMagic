using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface ISeatService
    {
        void AddSeat(Seat value);
        void DeleteSeat(Seat seat);
        bool ExistsSeat(int seatNo);
        void UpdateSeat(Seat seat);
    }
}