using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface IShowService
    {
        void AddShow(Show value);
        bool ExistsShow(string name);
    }
}