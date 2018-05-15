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
    public class FilmController : Controller
    {
        //MoviesAndMagicEntities db = new MoviesAndMagicEntities();
        private IRepository<Domain.Genre> genreRepository;
        private IRepository<Domain.Film> filmRepository;
        private readonly IUnitOfWork unitOfWork;
        private IFilmService filmService;


        public FilmController()
        {
            var dbFactory = new DatabaseFactory();
            this.filmRepository = new Repository<Domain.Film>(dbFactory);
            this.genreRepository = new Repository<Domain.Genre>(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
            this.filmService = new FilmService(dbFactory);
        }

        // GET: Film
        public ActionResult Index()
        {

            var films = filmRepository.GetAll().Select(f => new Models.Film()
            {
                Name = f.Name,
                Description = f.Description,
                Id = f.Id,
                GenreName = f.Genre.Name

            }).ToList();

            /*if (!String.IsNullOrEmpty(searchString))
            {
                films = films.Where(s => s.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }
            */
            return View(films);
        }

        // Get: Create
        [HttpGet]
        public ActionResult Create()
        {
            var model = new Models.Film();
            var genres = genreRepository.GetAll();
            var selectGenres = genres.Select(f => new SelectListItem()
            {
                Value = f.Id.ToString(),
                Text = f.Name

            }).ToList();
            ViewBag.Genres = selectGenres;
            return View(model);
        }

        // Post: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Film model)
        {
            if (ModelState.IsValid)
            {
                if (!filmService.ExistsFilm(model.Name))
                {
                    var dbModel = new Domain.Film();
                    dbModel.InjectFrom(model);
                    //filmRepository.Add(dbModel);
                    filmService.AddFilm(dbModel);
                    TempData["message"] = string.Format("{0} has been saved", model.Name);
                }
                else
                {
                    ModelState.AddModelError("Name", "This movie is already in the database!");

                    var genres = genreRepository.GetAll().Select(f => new SelectListItem()
                    {
                        Value = f.Id.ToString(),
                        Text = f.Name
                    }).ToList();
                    ViewBag.Genres = genres;

                    return View(model);
                }
                //transform the object
                //unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Film/Edit/5
        public ActionResult Edit(int id)
        {
            var film = filmRepository.GetById(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            var filmModel = new Models.Film();
            filmModel.InjectFrom(film);
            var genres = genreRepository.GetAll().Select(f => new SelectListItem()
            {
                Value = f.Id.ToString(),
                Text = f.Name,
                Selected = filmModel.GenreId == f.Id
            }).ToList();
            ViewBag.Genres = genres;
            return View(filmModel);
        }

        // POST: Film/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Film film)
        {
            if (ModelState.IsValid)
            {
                //filmRepository.Update(filmEdit);
                //unitOfWork.Commit();

                if (!filmService.ExistsFilm(film.Name))
                {
                    var filmDb = filmRepository.GetById(film.Id);
                    TryUpdateModel(filmDb);
                    filmService.UpdateFilm(filmDb);
                    TempData["message"] = string.Format("{0} has been saved", film.Name);
                }
                else
                {
                    ModelState.AddModelError("Name", "This movie is already in the database!");

                    var genres = genreRepository.GetAll().Select(f => new SelectListItem()
                    {
                        Value = f.Id.ToString(),
                        Text = f.Name
                    }).ToList();
                    ViewBag.Genres = genres;

                    return View(film);
                }
            }

            else
            {
                return View(film);
            }

            return RedirectToAction("Index");
        }

        // GET: Film/Delete/5
        public ActionResult Delete(int id)
        {
            var film = filmRepository.FindById(id);
            if (film == null)
            {
                return HttpNotFound();
            }

            var filmId = new Models.Film();
            filmId.InjectFrom(film);
            return View(filmId);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.Film film)
        {

            if (!filmService.IsUsedFilm(film.Id))
            {
                var filmDelete = filmRepository.FindById(id);
                //var userCheck = userRepository.GetAll().Count(u => u.Id == id);

                filmDelete.InjectFrom(film);
                filmService.DeleteFilm(filmDelete);
                TempData["message"] = string.Format("{0} has been saved", film.Name);
            }
            else
            {
                ModelState.AddModelError("Name", "Cannot delete this movie!");
            }

            //filmRepository.Delete(filmDelete);
            //unitOfWork.Commit();
            return RedirectToAction("Index");

            //return View(film);
        }
    }
}