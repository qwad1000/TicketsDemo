using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface ITicketRepository
    {
        void Create(Ticket ticket);
        void Update(Ticket ticket);
        Ticket Get(int id);
    }
}
