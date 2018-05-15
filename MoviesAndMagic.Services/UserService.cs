using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoviesAndMagic.Domain;
using MoviesAndMagic.Data.Infrastructure;
using System.Web.ModelBinding;

namespace MoviesAndMagic.Services
{


    public class UserService : IUserService
    {

        private DatabaseFactory factory;
        private IRepository<Domain.User> userRepository;
        private IRepository<Domain.Role> roleRepository;
        private IRepository<Domain.Reservation> reservationRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(DatabaseFactory factory)
        {
            this.factory = factory;
            this.userRepository = new Repository<Domain.User>(factory);
            this.roleRepository = new Repository<Domain.Role>(factory);
            this.reservationRepository = new Repository<Domain.Reservation>(factory);
            this.unitOfWork = new UnitOfWork(factory);
        }



        public void AddUser(User value)
        {
            var exists = userRepository.GetAll().Any(x => x.Firstname.ToLower() == value.Firstname.ToLower()
            && x.UserName.ToLower() == value.UserName.ToLower()
            && x.Email == value.Email
            && x.Lastname.ToLower() == value.Lastname.ToLower());
            if (!exists)
            {
                userRepository.Add(value);

                //transform the object
                unitOfWork.Commit();
            }
            else
            {
                throw new ArgumentException("User is already added in the database.");
            }

        }

        public bool ExistsUser(string email, string value)
        {
            var exists = userRepository.GetAll().Any(f => f.Email == email && f.UserName == value);

            if (!exists)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public void UpdateUser(User value)
        {
            userRepository.Update(value);
            unitOfWork.Commit();
        }

        public void DeleteUser(User value)
        {
            userRepository.Delete(value);
            unitOfWork.Commit();
        }
    }
}
