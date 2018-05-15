using MoviesAndMagic.Data.Infrastructure;
using MoviesAndMagic.Services;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoviesAndMagic.Areas.Admin.Controllers
{
    public class UserController : Controller
    {

        private IRepository<Domain.User> userRepository;
        private IRepository<Domain.Role> roleRepository;
        private IRepository<Domain.Reservation> reservationRepository;
        private readonly IUnitOfWork unitOfWork;
        private IUserService userService;

        public UserController()
        {
            var dbFactory = new DatabaseFactory();
            this.userRepository = new Repository<Domain.User>(dbFactory);
            this.roleRepository = new Repository<Domain.Role>(dbFactory);
            this.reservationRepository = new Repository<Domain.Reservation>(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
            this.userService = new UserService(dbFactory);
        }
        // GET: User
        public ActionResult Index()
        {
            //var users = userRepository.GetAll();
            //return View(users);
            var users = userRepository.GetAll().Select(u => new Models.User()
            {
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Email = u.Email,
                UserName = u.UserName,
                Id = u.Id
            }).ToList();
            return View(users);
        }

        // GET: User/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: User/Create
        public ActionResult Create()
        {
            var model = new Models.User();
            //Roles
            var roles = roleRepository.GetAll();
            var rolesList = roles.Select(r => new SelectListItem()
            {
                Value = r.Id.ToString(),
                Text = r.RoleName
            }).ToList();
            //Reservationss
            var reservations = reservationRepository.GetAll();
            var reservationList = reservations.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.ShowId.ToString()
            }).ToList();
            ViewBag.Roles = rolesList;
            ViewBag.ReservationId = reservationList;

            return View(model);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(Models.User model)
        {
            if (ModelState.IsValid)
            {
                if (!userService.ExistsUser(model.Email, model.UserName))
                {
                    var dbModel = new Domain.User();
                    dbModel.InjectFrom(model);
                    //filmRepository.Add(dbModel);
                    userService.AddUser(dbModel);
                    TempData["message"] = string.Format("{0} has been saved", model.Firstname);
                }
                else
                {
                    ModelState.AddModelError("Email", "Can't add an email that is already in the database!");
                    ModelState.AddModelError("UserName", "Can't add an email that is already in the database!");
                    //Roles
                    var roles = roleRepository.GetAll();
                    var rolesList = roles.Select(r => new SelectListItem()
                    {
                        Value = r.Id.ToString(),
                        Text = r.RoleName
                    }).ToList();
                    //Reservationss
                    var reservations = reservationRepository.GetAll();
                    var reservationList = reservations.Select(x => new SelectListItem()
                    {
                        Value = x.Id.ToString(),
                        Text = x.ShowId.ToString()
                    }).ToList();
                    ViewBag.Roles = rolesList;
                    ViewBag.ReservationId = reservationList;
                    return View(model);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {

            var user = userRepository.GetById(id);

            var userId = new Models.User();
            userId.InjectFrom(user);
            return View(userId);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.User user)
        {

            if (ModelState.IsValid)
            {
                var userEdit = new Domain.User();
                userEdit.InjectFrom(user);
                userService.UpdateUser(userEdit);
                TempData["message"] = string.Format("{0} has been saved", user.Firstname);
            }
            else
            {

                return View(user);
            }
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userRepository.FindById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var userId = new Models.User();
            userId.InjectFrom(user);
            return View(userId);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.User user)
        {
            var userDelete = userRepository.FindById(id);
            //var userCheck = userRepository.GetAll().Count(u => u.Id == id);

            userDelete.InjectFrom(user);
            userService.DeleteUser(userDelete);
            TempData["message"] = string.Format("{0} has been deleted", user.Firstname);
            return RedirectToAction("Index");


        }


    }


}