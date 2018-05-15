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
        public class CinemaController : Controller
        {
            private IRepository<Domain.Cinema> cinemaRepository;
            private readonly IUnitOfWork unitOfWork;
            private ICinemaService cinemaService;

            public CinemaController()
            {
                var dbFactory = new DatabaseFactory();
                this.cinemaRepository = new Repository<Domain.Cinema>(dbFactory);
                this.unitOfWork = new UnitOfWork(dbFactory);
                this.cinemaService = new CinemaService(dbFactory);

            }
            // GET: Cinema
            public ActionResult Index()
            {
                var cinemas = cinemaRepository.GetAll().Select(c => new Models.Cinema()
                {
                    Id = c.Id,
                    CinemaCity = c.CinemaCity

                }).ToList();

                return View(cinemas);
            }

            /* GET: Cinema/Details/5
            public ActionResult Details(int id)
            {
                return View();
            }*/

            // GET: Cinema/Create
            public ActionResult Create()
            {
                var model = new Models.Cinema();
                return View(model);
            }

            // POST: Cinema/Create
            [HttpPost]
            [ValidateAntiForgeryToken]

            public ActionResult Create(Models.Cinema model)
            {
                if (ModelState.IsValid)
                {
                    if (!cinemaService.ExistsCinema(model.CinemaCity))
                    {
                        var dbModel = new Domain.Cinema();
                        dbModel.InjectFrom(model);
                        cinemaService.AddCinema(dbModel);
                        TempData["message"] = string.Format("{0} has been saved", model.CinemaCity);
                }
                    else
                    {
                        ModelState.AddModelError("CinemaCity", "Cant add a cinema that is already in the database!");

                        //transform the object
                        //unitOfWork.Commit
                        return View(model);

                    }
                    return RedirectToAction("Index");
                }
                return View(model);
            }


            // GET: Cinema/Edit/5
            public ActionResult Edit(int id)
            {

                {
                    var cinema = cinemaRepository.GetById(id);
                    if (cinema == null)
                    {
                        return HttpNotFound();
                    }

                    var cinemaId = new Models.Cinema();
                    cinemaId.InjectFrom(cinema);
                    return View(cinemaId);
                }
            }

            // POST: Cinema/Edit/5
            [HttpPost]
            public ActionResult Edit(Models.Cinema cinema)
            {
                if (ModelState.IsValid)
                {
                    var dbCinema = new Domain.Cinema();
                    dbCinema.InjectFrom(cinema);
                    cinemaRepository.Update(dbCinema);
                    TempData["message"] = string.Format("{0} has been saved", cinema.CinemaCity);
                    unitOfWork.Commit();
                }
                else
                {
                    return View(cinema);
                }

                return RedirectToAction("Index");
            }

            // GET: User/Delete/5
            public ActionResult Delete(int id)
            {

                var cinema = cinemaRepository.FindById(id);
                if (cinema == null)
                {
                    return HttpNotFound();
                }
                var cinemaId = new Models.Cinema();
                cinemaId.InjectFrom(cinema);
                return View(cinemaId);

            }

            // POST: User/Delete/5
            [HttpPost]
            public ActionResult Delete(int id, Models.Cinema cinema)
            {

                var cinemaDelete = cinemaRepository.FindById(id);
                cinemaDelete.InjectFrom(cinema);
                cinemaRepository.Delete(cinemaDelete);
                TempData["message"] = string.Format("{0} has been saved", cinema.CinemaCity);
                unitOfWork.Commit();
                return RedirectToAction("Index");

            }
        }
    }
