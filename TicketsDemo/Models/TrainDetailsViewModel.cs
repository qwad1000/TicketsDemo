using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class TrainDetailsViewModel
    {
        public DateTime ScheduleStart { get; set; }
        public DateTime ScheduleEnd { get; set; }
        public Train Train { get; set; }
        public List<Run> Runs { get; set; }
        public List<DateTime> AvailableDates { get; set; }
    }
}