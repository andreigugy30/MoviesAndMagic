using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface IFilmService
    {
        void AddFilm(Film value);
        void UpdateFilm(Film value);
        bool ExistsFilm(string name);
        bool IsUsedFilm(int id);
        void DeleteFilm(Film value);
    }
}