using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public enum TicketStatusEnum { Active=1, Sold=2 }

    public class Ticket
    {
        public int Id { get; set; }
        public TicketStatusEnum Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public List<PriceComponent> PriceComponents { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
