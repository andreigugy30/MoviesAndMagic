using System;
using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface IShowTimeFilmService
    {
        void AddShowTimeFilm(ShowTimeFilm value);
        bool ExistsStf(DateTime showTime);
    }
}