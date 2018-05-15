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
    public class GenreController : Controller
    {
        private IRepository<Domain.Genre> genreRepository;
        private IRepository<Domain.Film> filmRepository;
        private readonly IUnitOfWork unitOfWork;
        private IGenreService genreService;
        //MoviesAndMagicEntities dbContext = new MoviesAndMagicEntities();

        public GenreController()
        {
            var dbFactory = new DatabaseFactory();
            this.filmRepository = new Repository<Domain.Film>(dbFactory);
            this.genreRepository = new Repository<Domain.Genre>(dbFactory);
            this.genreService = new GenreService(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
        }

        // GET: Genre
        public ActionResult Index()
        {

            var genres = genreRepository.GetAll().Select(g => new Models.Genre()
            {
                Id = g.Id,
                Name = g.Name

            }).ToList();

            return View(genres);
        }

        // GET: Genre/Details/5
        /* public ActionResult Details(int id)
         {
             return View();
         }*/

        // GET: Genre/Create
        public ActionResult Create()
        {

            var model = new Models.Genre();
            //var films = filmRepository.GetAll();
            //var selectFilms = films.Select(f => new SelectListItem()
            //{
            //    Value = f.Id.ToString(),
            //    Text = f.Name
            //}).ToList();
            //ViewBag.Films = selectFilms;
            return View(model);
        }

        // POST: Genre/Create
        [HttpPost]
        public ActionResult Create(Models.Genre model)
        {
            if (ModelState.IsValid)
            {
                if (!genreService.ExistsGenre(model.Name))
                {
                    var dbModel = new Domain.Genre();
                    dbModel.InjectFrom(model);
                    genreService.AddGenre(dbModel);
                    TempData["message"] = string.Format("{0} has been saved", model.Name);
                }

                else
                {
                    ModelState.AddModelError("Name", "This genre is already in the database!!");

                    return View(model);
                }
                //genreRepository.Add(dbModel);

                //transform the object
                //unitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Genre/Edit/5
        public ActionResult Edit(int id)
        {
            {
                var genre = genreRepository.GetById(id);
                if (genre == null)
                {
                    return HttpNotFound();
                }

                var genreId = new Models.Genre();
                genreId.InjectFrom(genre);
                return View(genreId);
            }
        }

        // POST: Genre/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Genre genre)
        {
            if (ModelState.IsValid)
            {
                var dbGenre = new Domain.Genre();
                dbGenre.InjectFrom(genre);
                genreRepository.Update(dbGenre);
                TempData["message"] = string.Format("{0} has been saved", genre.Name);
                unitOfWork.Commit();
            }
            else
            {
                return View(genre);
            }

            return RedirectToAction("Index");
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int id)
        {

            var genre = genreRepository.FindById(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            var genreId = new Models.Genre();
            genreId.InjectFrom(genre);
            return View(genreId);

        }

        // POST: Genre/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.Genre genre)
        {

            var genreDelete = genreRepository.FindById(id);
            genreDelete.InjectFrom(genre);
            genreRepository.Delete(genreDelete);
            TempData["message"] = string.Format("{0} has been saved", genre.Name);
            unitOfWork.Commit();
            return RedirectToAction("Index");

        }
    }
}