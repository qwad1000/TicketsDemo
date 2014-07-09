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
    public class ScheduleController : Controller
    {
        private ISchedule _schedule;
        private ITrainRepository _trainRepo;
        private IRunRepository _runRepo;

        public ScheduleController(ISchedule schedule, ITrainRepository trainRepo, IRunRepository runRepo) {
            _schedule = schedule;
            _trainRepo = trainRepo;
            _runRepo = runRepo;
        }

        public ActionResult Index()
        {
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(1);
            var model = new IndexPageViewModel()
            {
                ScheduleStart = startDate,
                ScheduleEnd = endDate,
                Runs = _runRepo.GetRuns(startDate, endDate),
                Trains = _trainRepo.GetAllTrains().ToDictionary(x => x.Id)
            };

            return View(model);
        }

        public ActionResult Train(int id) {
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(1);
            var model = new TrainDetailsViewModel()
            {
                ScheduleStart = startDate,
                ScheduleEnd = endDate,
                Runs = _runRepo.GetRuns(startDate, endDate,id),
                Train = _trainRepo.GetTrainDetails(id),
                AvailableDates = _schedule.GetAvailableDatesForNewRun(id,startDate,endDate)
            };
            
            return View(model);      
        }
        
        public ActionResult CreateRun(int trainId, DateTime date) {
            _schedule.CreateRun(trainId, date);
            return RedirectToAction("Train", new { id = trainId });
        }

        public ActionResult DeleteRun(int id)
        {
            _schedule.DeleteRun(id);
            return RedirectToAction("Train", new { id = id });
        }
    }
}
