using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public interface ITicketService
    {
        Ticket CreateTicket(int reservationId,string firstName, string lastName);
        void SellTicket(Ticket ticket);
    }
}
