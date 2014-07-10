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
    public class TicketService : ITicketService
    {
        private ITicketRepository _tickRepo;
        private IPriceCalculationStrategy _priceStr;
        private IReservationRepository _resRepo;
        private ILogger _logger;

        public TicketService(ITicketRepository tickRepo, IReservationRepository resRepo,
            IPriceCalculationStrategy priceCalculationStrategy, ILogger logger)
        {
            _tickRepo = tickRepo;
            _resRepo = resRepo;
            _priceStr = priceCalculationStrategy;
            _logger = logger;
        }

        public Ticket CreateTicket(int reservationId ,string fName,string lName)
        {
            var res = _resRepo.Get(reservationId);

            var newTicket = new Ticket()
            {
                ReservationId = res.Id,
                Reservation = res,
                CreatedDate = DateTime.Now,
                FirstName = fName,
                LastName = lName,
                Status = TicketStatusEnum.Active,
                PriceComponents = new List<PriceComponent>()
            };

            newTicket.PriceComponents = _priceStr.CalculatePrice(res);

            _tickRepo.Create(newTicket);
            _logger.Log(String.Format("ticket {0} created",newTicket), LogSeverity.Info);
            return newTicket;
        }

        public void SellTicket(Ticket ticket)
        {
            if (ticket.Status == TicketStatusEnum.Sold)
            {
                _logger.Log(String.Format("ticket {0} is already sold",ticket), LogSeverity.Info);
                throw new ArgumentException("ticket is already sold");
            }

            ticket.Status = TicketStatusEnum.Sold;
            _tickRepo.Update(ticket);
            _logger.Log(String.Format("ticket {0} sold", ticket), LogSeverity.Info);
        }
    }
}
