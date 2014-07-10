using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class ReservationService : IReservationService
    {
        IReservationRepository _resRepo;
        private ILogger _logger;

        public ReservationService(IReservationRepository resRepo, ILogger logger)
        {
            _resRepo = resRepo;
            _logger = logger;
        }


        public Reservation Reserve(PlaceInRun place)
        {
            if (PlaceIsOccupied(place))
                throw new InvalidOperationException(String.Format("place {0} can't be reserved becouse it is currently occupied", place.Id));

            var createIt = new Reservation()
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddMinutes(20),
                PlaceInRunId = place.Id,
                PlaceInRun = place
            };

            _resRepo.Create(createIt);
            _logger.Log(String.Format("reservation {0} created", createIt), LogSeverity.Info);

            return createIt;
        }

        public void RemoveReservation(Reservation reservation)
        {
            reservation.End = DateTime.Now;
            _resRepo.Update(reservation);
            //_logger.Log(String.Format("reservation {0} removed", reservation), LogSeverity.Info);
        }

        public bool PlaceIsOccupied(PlaceInRun place)
        {
            if(place.Reservations == null)
                return false;

            var activeReservationFound = false;
            foreach(var res in place.Reservations){
                if(res.Ticket != null || (res.Start < DateTime.Now && res.End > DateTime.Now))
                {
                    activeReservationFound = true;
                }
            }

            return activeReservationFound;
        }
    }
}
