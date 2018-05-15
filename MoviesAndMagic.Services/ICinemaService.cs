using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface ICinemaService
    {

        void AddCinema(Cinema value);

        bool ExistsCinema(string name);

    }

}