using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
         ITrainRepository _trainRepo;

         public ReservationRepository(ITrainRepository trainRepo)
        {
            _trainRepo = trainRepo;
        }

        public void Create(Reservation reservation)
        {
            using (var ctx = new TicketsContext())
            {
                if (reservation.PlaceInRun != null) {
                    ctx.PlacesInRuns.Attach(reservation.PlaceInRun);
                    ctx.Entry(reservation.PlaceInRun).State = System.Data.Entity.EntityState.Unchanged;
                }

                ctx.Reservations.Add(reservation);
                ctx.SaveChanges();
            }
        }

        public void Update(Reservation reservation)
        {
            using (var ctx = new TicketsContext())
            {
                ctx.Reservations.Attach(reservation);
                ctx.Entry(reservation).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public Reservation Get(int id)
        {
            using (var ctx = new TicketsContext())
            {
                var res = ctx.Reservations
                    .Include("PlaceInRun")
                    //.Include("PlaceInRun.Place")
                    .Include("PlaceInRun.Run")
                    //.Include("PlaceInRun.Run.Train")
                    //.Include("PlaceInRun.Place.Carriage")
                    .Where(r => r.Id == id).Single();

                var train = _trainRepo.GetTrainDetails(res.PlaceInRun.Run.TrainId);

                foreach (var car in train.Carriages)
                {
                    var place = car.Places.FirstOrDefault(p => p.Id == res.PlaceInRun.PlaceId);
                    if (place != null)
                    {
                        res.PlaceInRun.Place = place;
                    }
                }
                res.PlaceInRun.Run.Train = train;

                return res;
            }
        }
    }
}
