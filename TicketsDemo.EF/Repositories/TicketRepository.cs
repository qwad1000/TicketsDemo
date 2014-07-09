using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        ITrainRepository _trainRepo;

        public TicketRepository(ITrainRepository trainRepo)
        {
            _trainRepo = trainRepo;
        }

        public void Create(Data.Entities.Ticket ticket)
        {
            using (var ctx = new TicketsContext())
            {
                if (ticket.Reservation != null)
                {
                    ctx.Reservations.Attach(ticket.Reservation);
                    ctx.Entry(ticket.Reservation).State = System.Data.Entity.EntityState.Unchanged;
                }

                ctx.Tickets.Add(ticket);
                ctx.SaveChanges();
            }
        }

        public void Update(Data.Entities.Ticket ticket)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Tickets.Attach(ticket);
                ctx.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public Ticket Get(int id) {
            using (var ctx = new TicketsContext())
            {
                var ticket =  ctx.Tickets
                    .Include("PriceComponents")
                    .Include("Reservation")
                    .Include("Reservation.PlaceInRun")
                    .Include("Reservation.PlaceInRun.Run").Where(r => r.Id == id).Single();

                var train = _trainRepo.GetTrainDetails(ticket.Reservation.PlaceInRun.Run.TrainId);
                
                foreach(var car in train.Carriages){
                    var place = car.Places.FirstOrDefault(p => p.Id == ticket.Reservation.PlaceInRun.PlaceId);
                    if(place != null){
                        ticket.Reservation.PlaceInRun.Place = place;
                    }
                }
                ticket.Reservation.PlaceInRun.Run.Train = train;
                return ticket;
            }
        }

    }
}
