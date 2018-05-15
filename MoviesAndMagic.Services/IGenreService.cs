using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface IGenreService
    {
        void AddGenre(Genre value);
        bool ExistsGenre(string name);
    }
}