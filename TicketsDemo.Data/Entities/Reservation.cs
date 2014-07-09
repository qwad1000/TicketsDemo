using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int PlaceInRunId { get; set; }
        public PlaceInRun PlaceInRun { get; set; }
        public int? TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
