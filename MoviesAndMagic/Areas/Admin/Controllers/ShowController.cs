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
    public class ShowController : Controller
    {
        private IRepository<Domain.Show> showRepository;
        private IRepository<Domain.Cinema> cinemaRepository;
        private IRepository<Domain.Film> filmRepository;
        private IRepository<Domain.ShowTimeFilm> showtimefilmRepository;
        private readonly IUnitOfWork unitOfWork;
        private IShowService showService;

        public ShowController()
        {
            var dbFactory = new DatabaseFactory();
            this.showRepository = new Repository<Domain.Show>(dbFactory);
            this.cinemaRepository = new Repository<Domain.Cinema>(dbFactory);
            this.filmRepository = new Repository<Domain.Film>(dbFactory);
            this.showtimefilmRepository = new Repository<Domain.ShowTimeFilm>(dbFactory);
            this.showService = new ShowService(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
        }

        // GET: Show
        public ActionResult Index(string searchString)
        {
            //var shows = showRepository.GetAll().Select(s => new ViewModels.Show()
            //{
            //    Id = s.Id,
            //   /* CinemaCity = s.Cinema.Shows.ToString()*/
            //    FilmName = s,
            //    ShowTimeFilm = s.ShowTimeFilm.ShowTime.ToString()


            //}).ToList();
            //return View(shows);
            /* var shows = showRepository.GetAll();
             IEnumerable<Show> showsIndex = shows.Select(s => new Show()*/
            var shows = showRepository.GetAll().Select(s => new Models.Show()
            {
                Id = s.Id,
                CinemaCity = s.Cinema.CinemaCity,
                FilmName = s.Film.Name,
                ShowTimeFilm = s.ShowTimeFilm.ShowTime.ToString("dd/MM/yyyy hh:mm")

            }).ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                shows = shows.Where(s => s.FilmName.ToLower().Contains(searchString.ToLower())).ToList();
            }

            return View(shows);

        }

        // Get: Create 
        [HttpGet]
        public ActionResult Create()
        {
            var model = new Models.Show();

            //Cinema
            var cinemas = cinemaRepository.GetAll();
            var cinemaList = cinemas.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.CinemaCity
            }).ToList();
            //Film
            var films = filmRepository.GetAll();
            var filmList = films.Select(f => new SelectListItem()
            {
                Value = f.Id.ToString(),
                Text = f.Name
            }).ToList();
            //ShowTimeFilm
            var showtimefilms = showtimefilmRepository.GetAll();
            var showtimefilmList = showtimefilms.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.ShowTime.ToString("dd/MM/yyyy hh:mm")
            }).ToList();

            ViewBag.CinemaId = cinemaList;
            ViewBag.FilmId = filmList;
            ViewBag.ShowTimeFilmId = showtimefilmList;

            return View(model);
        }

        // Post: Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(Models.Show model)
        {
            if (ModelState.IsValid)
            {
                if (!(showService.ExistsShow(model.CinemaCity) && showService.ExistsShow(model.FilmName) && showService.ExistsShow(model.ShowTimeFilm)))
                {
                    var dbModel = new Domain.Show();
                    dbModel.InjectFrom(model);
                    showService.AddShow(dbModel);
                    TempData["message"] = string.Format("{0} has been saved", model.FilmName);
                }
                else
                {
                    ModelState.AddModelError("Show", "Cannot have two identical shows!");

                    //Cinema
                    var cinemas = cinemaRepository.GetAll();
                    var cinemaList = cinemas.Select(c => new SelectListItem()
                    {
                        Value = c.Id.ToString(),
                        Text = c.CinemaCity
                    }).ToList();
                    //Film
                    var films = filmRepository.GetAll();
                    var filmList = films.Select(f => new SelectListItem()
                    {
                        Value = f.Id.ToString(),
                        Text = f.Name
                    }).ToList();
                    //ShowTimeFilm
                    var showtimefilms = showtimefilmRepository.GetAll();
                    var showtimefilmList = showtimefilms.Select(s => new SelectListItem()
                    {
                        Value = s.Id.ToString(),
                        Text = s.ShowTime.ToString("dd/MM/yyyy hh:mm")
                    }).ToList();

                    ViewBag.Cinemas = cinemaList;
                    ViewBag.Films = filmList;
                    ViewBag.ShowTimeFilms = showtimefilmList;

                    return View(model);
                }

                //showRepository.Add(dbModel);

                //transform the object
                //unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Show/Edit/5
        public ActionResult Edit(int id)
        {
            var shows = showRepository.GetById(id);
            if (shows == null)
            {
                return HttpNotFound();
            }

            var showsView = new Models.Show()
            {
                Id = shows.Id,
                CinemaCity = shows.Cinema.CinemaCity,
                FilmName = shows.Film.Name,
                ShowTimeFilm = shows.ShowTimeFilm.ShowTime.ToString("dd/MM/yyyy hh:mm")

            };

            //Cinema
            var cinemas = cinemaRepository.GetAll();
            var cinemaList = cinemas.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.CinemaCity
            }).ToList();
            //Film
            var films = filmRepository.GetAll();
            var filmList = films.Select(f => new SelectListItem()
            {
                Value = f.Id.ToString(),
                Text = f.Name
            }).ToList();
            //ShowTimeFilm
            var showtimefilms = showtimefilmRepository.GetAll();
            var showtimefilmList = showtimefilms.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.ShowTime.ToString("dd/MM/yyyy hh:mm")
            }).ToList();

            ViewBag.Cinemas = cinemaList;
            ViewBag.Films = filmList;
            ViewBag.ShowTimeFilms = showtimefilmList;

            return View(showsView);
        }

        // POST: Show/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Show show)
        {
            if (ModelState.IsValid)
            {
                var showEdit = new Domain.Show();
                showEdit.InjectFrom(show);
                showRepository.Update(showEdit);
                TempData["message"] = string.Format("{0} has been saved", show.FilmName);
                unitOfWork.Commit();

            }

            else
            {
                return View(show);
            }

            return RedirectToAction("Index");
        }

        // GET: Show/Delete/5
        public ActionResult Delete(int id)
        {
            //var show = showRepository.FindById(id);
            //if (showRepository == null)
            //{
            //    return HttpNotFound();
            //}

            //var showId = new Show();
            //showId.InjectFrom(show);
            //return View(showId);

            var showsToDelete = showRepository.GetById(id);
            if (showsToDelete == null)
            {
                return HttpNotFound();
            }

            var showsView = new Models.Show()
            {
                CinemaCity = showsToDelete.Cinema.CinemaCity,
                FilmName = showsToDelete.Film.Name,
                ShowTimeFilm = showsToDelete.ShowTimeFilm.ShowTime.ToString()
            };

            return View(showsView);

        }

        // POST: Show/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.Show show)
        {
            var showDelete = showRepository.FindById(id);

            showDelete.InjectFrom(show);
            showRepository.Delete(showDelete);
            TempData["message"] = string.Format("{0} has been saved", show.FilmName);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}