using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public interface IReservationService
    {
        Reservation Reserve(PlaceInRun place);
        void RemoveReservation(Reservation reservation);
        bool PlaceIsOccupied(PlaceInRun place);
    }
}
