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
    public class ShowTimeFilmController : Controller
    {
        private IRepository<Domain.ShowTimeFilm> showTimeFilmRepository;
        private IRepository<Domain.Show> showRepository;
        private IShowTimeFilmService showTimeFilmService;
        private readonly IUnitOfWork unitOfWork;

        public ShowTimeFilmController()
        {
            var dbFactory = new DatabaseFactory();
            this.showTimeFilmRepository = new Repository<Domain.ShowTimeFilm>(dbFactory);
            this.showRepository = new Repository<Domain.Show>(dbFactory);
            this.showTimeFilmService = new ShowTimeFilmService(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
        }
        // GET: ShowTimeFilm
        public ActionResult Index()
        {
            var showTimeFilm = showTimeFilmRepository.GetAll().Select(s => new Models.ShowTimeFilm()
            {
                Id = s.Id,
                ShowTime = s.ShowTime,

            }).ToList();
            return View(showTimeFilm);
        }

        /* GET: ShowTimeFilm/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: ShowTimeFilm/Create
        public ActionResult Create()
        {
            var model = new Models.ShowTimeFilm();

            return View(model);
        }

        // POST: ShowTimeFilm/Create
        [HttpPost]
        public ActionResult Create(Models.ShowTimeFilm model)
        {
            if (ModelState.IsValid)
            {
                if (!showTimeFilmService.ExistsStf(model.ShowTime))
                {
                    var dbModel = new Domain.ShowTimeFilm();
                    dbModel.InjectFrom(model);
                    showTimeFilmService.AddShowTimeFilm(dbModel);
                    TempData["message"] = string.Format("{0} has been saved", model.ShowTime);
                }
                else
                {
                    ModelState.AddModelError("ShowTime", "Cant add a show time at the same hour in the same cinema!");
                    return View(model);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: ShowTimeFilm/Edit/5
        public ActionResult Edit(int id)
        {
            {
                var showTimeFilm = showTimeFilmRepository.GetById(id);
                if (showTimeFilm == null)
                {
                    return HttpNotFound();
                }

                var showTimeFilmId = new Models.ShowTimeFilm();
                showTimeFilmId.InjectFrom(showTimeFilm);
                return View(showTimeFilmId);
            }
        }

        // POST: ShowTimeFilm/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ShowTimeFilm showTimeFilm)
        {
            if (ModelState.IsValid)
            {
                var dbShowTimeFilm = new Domain.ShowTimeFilm();
                dbShowTimeFilm.InjectFrom(showTimeFilm);
                showTimeFilmRepository.Update(dbShowTimeFilm);
                TempData["message"] = string.Format("{0} has been saved", showTimeFilm.ShowTime);
                unitOfWork.Commit();
            }
            else
            {
                return View(showTimeFilm);
            }

            return RedirectToAction("Index");
        }

        // GET: ShowTimeFilm/Delete/5
        public ActionResult Delete(int id)
        {

            var showTimeFilm = showTimeFilmRepository.FindById(id);
            if (showTimeFilm == null)
            {
                return HttpNotFound();
            }
            var showTimeFilmId = new Models.ShowTimeFilm();
            showTimeFilmId.InjectFrom(showTimeFilm);
            return View(showTimeFilmId);

        }

        // POST: ShowTimeFilm/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.ShowTimeFilm showTimeFilm)
        {

            var showTimeFilmDelete = showTimeFilmRepository.FindById(id);
            showTimeFilmDelete.InjectFrom(showTimeFilm);
            showTimeFilmRepository.Delete(showTimeFilmDelete);
            TempData["message"] = string.Format("{0} has been saved", showTimeFilm.ShowTime);
            unitOfWork.Commit();
            return RedirectToAction("Index");

        }
    }
}