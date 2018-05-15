using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Data.Infrastructure
{
    public interface IDatabaseFactory
    {
        MoviesAndMagicEntities Get();
        void Dispose();
    }
}