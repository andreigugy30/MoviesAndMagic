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
    public class ReservationController : Controller
    {
        private IRepository<Domain.Reservation> reservationRepository;
        private IRepository<Domain.Show> showRepository;
        private IRepository<Domain.Seat> seatRepository;
        private readonly IUnitOfWork unitOfWork;
        private IReservationService reservationService;

        public ReservationController()
        {
            var dbFactory = new DatabaseFactory();
            this.reservationRepository = new Repository<Domain.Reservation>(dbFactory);
            this.showRepository = new Repository<Domain.Show>(dbFactory);
            this.seatRepository = new Repository<Domain.Seat>(dbFactory);
            this.unitOfWork = new UnitOfWork(dbFactory);
            this.reservationService = new ReservationService(dbFactory);
        }

        // GET: Reservation
        public ActionResult Index()
        {
            var reservations = reservationRepository.GetAll().Select(r => new Models.Reservation()
            {
                //ShowId = r.Id.ToString(),
                SeatNo = r.Seat.SeatNo,
                ShowName = r.Show.Film.Name,
                Id = r.Id
            }).ToList();
            return View(reservations);
        }

        // Get: Create 
        [HttpGet]
        public ActionResult Create()
        {
            var model = new Models.Reservation();

            //Reservation
            var reservations = reservationRepository.GetAll();
            var reservationList = reservations.Select(r => new SelectListItem()
            {
                Value = r.Id.ToString()
            }).ToList();
            //Seat
            var seats = seatRepository.GetAll();
            var seatList = seats.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.SeatNo.ToString()
            }).ToList();

            //Show

            var shows = showRepository.GetAll();
            var showList = shows.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Film.Name
            }).ToList();

            ViewBag.Reservations = reservationList;
            ViewBag.Seats = seatList;
            ViewBag.Shows = showList;

            return View(model);
        }

        // Post: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Models.Reservation model)
        {
            if (ModelState.IsValid)
            {
                if (!reservationService.ExistsReservation(model.ShowId, model.SeatId))
                {
                    var dbModel = new Domain.Reservation();
                    dbModel.InjectFrom(model);
                    reservationService.AddReservation(dbModel/*, dbModel1*/);
                    TempData["message"] = string.Format("{0} has been saved", model.ShowName);
                }
                else
                {
                    ModelState.AddModelError("SeatId", "This seat is already booked !");
                    ModelState.AddModelError("ShowId", "This show is already booked !");

                    //Seat
                    var seats = seatRepository.GetAll();
                    var seatList = seats.Select(s => new SelectListItem()
                    {
                        Value = s.Id.ToString(),
                        Text = s.SeatNo.ToString()
                    }).ToList();

                    //Show

                    var shows = showRepository.GetAll();
                    var showList = shows.Select(s => new SelectListItem()
                    {
                        Value = s.Id.ToString(),
                        Text = s.Film.Name
                    }).ToList();

                    ViewBag.Seats = seatList;
                    ViewBag.Shows = showList;
                    return View(model);
                }

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int id)
        {
            var reservation = reservationRepository.GetById(id);
            //Seat
            var seats = seatRepository.GetAll();
            var Seats = seats.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.SeatNo.ToString()
            }).ToList();

            //Show

            var shows = showRepository.GetAll();
            var Shows = shows.Select(s => new SelectListItem()
            {
                Value = s.Id.ToString(),
                Text = s.Film.Name
            }).ToList();

            ViewBag.Seats = Seats;
            ViewBag.Shows = Shows;

            if (reservation == null)
            {
                return HttpNotFound();
            }

            var reservationId = new Models.Reservation();
            reservationId.InjectFrom(reservation);
            return View(reservationId);

        }

        // POST: Reservation/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                var exists = reservationRepository.GetAll().Any(f => f.ShowId == reservation.ShowId && f.SeatId == reservation.SeatId);
                if (!exists)
                {
                    var reservationDb = reservationRepository.GetById(reservation.Id);
                    TryUpdateModel(reservationDb);
                    reservationService.UpdateReservation(reservationDb);
                    TempData["message"] = string.Format("{0} has been saved", reservation.ShowName);
                }

                else
                {
                    ModelState.AddModelError("SeatId", "This seat is already booked !");
                    ModelState.AddModelError("ShowId", "This show is already booked !");
                    //Seat
                    var seats = seatRepository.GetAll();
                    var seatList = seats.Select(s => new SelectListItem()
                    {
                        Value = s.Id.ToString(),
                        Text = s.SeatNo.ToString()
                    }).ToList();

                    //Show

                    var shows = showRepository.GetAll();
                    var showList = shows.Select(s => new SelectListItem()
                    {
                        Value = s.Id.ToString(),
                        Text = s.Film.Name
                    }).ToList();

                    ViewBag.Seats = seatList;
                    ViewBag.Shows = showList;
                    return View(reservation);
                }
            }
            else
            {
                return View(reservation);
            }

            return RedirectToAction("Index");
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            var reservation = reservationRepository.FindById(id);
            if (reservationRepository == null)
            {
                return HttpNotFound();
            }

            var reservationId = new Models.Reservation();
            reservationId.InjectFrom(reservation);
            return View(reservationId);
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Models.Reservation reservation)
        {
            var reservationDelete = reservationRepository.FindById(id);

            reservationDelete.InjectFrom(reservation);
            reservationRepository.Delete(reservationDelete);
            TempData["message"] = string.Format("{0} has been saved", reservation.ShowName);
            unitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}