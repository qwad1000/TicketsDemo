using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Models;

namespace TicketsDemo.Controllers
{
    public class RunController : Controller
    {
        private ITicketRepository _tickRepo;
        private IRunRepository _runRepo;
        private IReservationService _resServ;
        private ITicketService _tickServ;
        private IPriceCalculationStrategy _priceCalc;

        public RunController(ITicketRepository tick, IRunRepository run, 
            IReservationService resServ,
            ITicketService tickServ,IPriceCalculationStrategy priceCalcStrategy) {
            _tickRepo = tick;
            _runRepo = run;
            _resServ = resServ;
            _tickServ = tickServ;
            _priceCalc = priceCalcStrategy;
        }

        public ActionResult Index(int id) {
            var run = _runRepo.GetRunDetails(id);
            var model = new RunViewModel() { 
                 Run = run,
                 ReservedPlaces = run.Places.Where(p => _resServ.PlaceIsOccupied(p)).Select(p => p.Id).ToList()
            };

            return View(model);
        }

        public ActionResult ReservePlace(int placeId,int runId) {
            var run = _runRepo.GetRunDetails(runId);
            var place = run.Places.Single(p => p.Id == placeId);

            var reservation = _resServ.Reserve(place);

            var model = new ReservationViewModel()
            {
                Reservation = reservation,
                PriceComponents = _priceCalc.CalculatePrice(reservation)
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTicket(CreateTicketModel model)
        {
            var tick = _tickServ.CreateTicket(model.ReservationId,model.FirstName,model.LastName);
            return RedirectToAction("Ticket", new { id = tick.Id });
        }

        public ActionResult Ticket(int id)
        {
            return View(_tickRepo.Get(id));
        }

    }
}