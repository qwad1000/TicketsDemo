using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class IndexPageViewModel
    {
        public DateTime ScheduleStart { get; set; }
        public DateTime ScheduleEnd { get; set; }
        public List<Run> Runs { get; set; }
        public Dictionary<int,Train> Trains { get; set; }
    }
}