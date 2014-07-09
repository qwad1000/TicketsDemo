using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Models
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public List<PriceComponent> PriceComponents { get; set; }
    }
}