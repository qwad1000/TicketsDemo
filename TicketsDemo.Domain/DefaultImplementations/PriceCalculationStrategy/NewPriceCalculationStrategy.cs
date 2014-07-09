using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class NewPriceCalculationStrategy : IPriceCalculationStrategy
    {
        public List<PriceComponent> CalculatePrice(Reservation reserv)
        {
            var components = new List<PriceComponent>();

            var placeComponentPrice = reserv.PlaceInRun.Place.Carriage.DefaultPrice * reserv.PlaceInRun.Place.PriceMultiplier;
            var placeComponent = new PriceComponent() { Name = "Main price" };

            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday || DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                placeComponentPrice = placeComponentPrice * 1.3m;
            }

            placeComponent.Value = placeComponentPrice;
            components.Add(placeComponent);


            if (placeComponent.Value > 30)
            {
                var cashDeskComponent = new PriceComponent()
                {
                    Name = "Cash desk service tax",
                    Value = placeComponent.Value * 0.2m
                };
                components.Add(cashDeskComponent);
            }

            return components;
        }
    }
}
