using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class RunViewModel
    {
        public Run Run { get; set; }
        public List<int> ReservedPlaces { get; set; }
    }
}