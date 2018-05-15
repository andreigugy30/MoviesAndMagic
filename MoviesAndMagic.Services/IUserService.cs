using MoviesAndMagic.Domain;

namespace MoviesAndMagic.Services
{
    public interface IUserService
    {
        void AddUser(User value);
        void DeleteUser(User value);
        bool ExistsUser(string email, string value);
        void UpdateUser(User value);
       
    }
}