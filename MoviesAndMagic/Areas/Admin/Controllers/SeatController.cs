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
        public class SeatController : Controller
        {
            private IRepository<Domain.Seat> seatRepository;
            private readonly IUnitOfWork unitOfWork;
            private ISeatService seatService;

            public SeatController()
            {
                var dbFactory = new DatabaseFactory();
                this.seatRepository = new Repository<Domain.Seat>(dbFactory);
                this.unitOfWork = new UnitOfWork(dbFactory);
                this.seatService = new SeatService(dbFactory);
            }

            // GET: Seat
            public ActionResult Index()
            {
                var seats = seatRepository.GetAll().Select(s => new Models.Seat()
                {
                    SeatNo = s.SeatNo,
                    Status = s.Status,
                    Price = s.Price,
                    Id = s.Id
                }).ToList();
                return View(seats);
            }

            // Get: Create
            [HttpGet]
            public ActionResult Create()
            {
                var model = new Models.Seat();

                return View(model);
            }

            // Post: Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(Models.Seat model)
            {
                if (ModelState.IsValid)
                {
                    if (!seatService.ExistsSeat(model.SeatNo))
                    {
                        var dbModel = new Domain.Seat();
                        dbModel.InjectFrom(model);
                        //filmRepository.Add(dbModel);
                        seatService.AddSeat(dbModel);
                        TempData["message"] = string.Format("{0} has been saved", model.SeatNo);
                }
                    else
                    {
                        ModelState.AddModelError("SeatNo", "Can't add a seat number that is already in the database!");
                        return View(model);
                    }

                    return RedirectToAction("Index");
                }

                return View(model);
            }

            // GET: Seat/Edit/5
            public ActionResult Edit(int id)
            {
                var seat = seatRepository.GetById(id);
                if (seat == null)
                {
                    return HttpNotFound();
                }

                var seatId = new Models.Seat();
                seatId.InjectFrom(seat);
                return View(seatId);
            }

            // POST: Seat/Edit/5
            [HttpPost]
            public ActionResult Edit(Models.Seat seat)
            {
                if (ModelState.IsValid)
                {
                    var seatEdit = new Domain.Seat();
                    seatEdit.InjectFrom(seat);
                    seatService.UpdateSeat(seatEdit);
                TempData["message"] = string.Format("{0} has been saved", seat.SeatNo);
            }

                else
                {
                    return View(seat);
                }

                return RedirectToAction("Index");
            }

            // GET: Seat/Delete/5
            public ActionResult Delete(int id)
            {
                var seat = seatRepository.FindById(id);
                if (seat == null)
                {
                    return HttpNotFound();
                }

                var seatId = new Models.Seat();
                seatId.InjectFrom(seat);
                return View(seatId);
            }

            // POST: Seat/Delete/5
            [HttpPost]
            public ActionResult Delete(int id, Models.Seat seat)
            {
                var seatDelete = seatRepository.FindById(id);

                seatDelete.InjectFrom(seat);
                seatService.DeleteSeat(seatDelete);
                TempData["message"] = string.Format("{0} has been saved", seat.SeatNo);
                return RedirectToAction("Index");
            }
        }
    }
