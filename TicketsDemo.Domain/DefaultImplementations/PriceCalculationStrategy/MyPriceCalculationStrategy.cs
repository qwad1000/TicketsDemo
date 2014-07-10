using System.Collections.Generic;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy
{
    public class MyPriceCalculationStrategy : IPriceCalculationStrategy
    {
        public decimal TeaPrice { get; set; }
        public decimal BedPrice { get; set; }

        public MyPriceCalculationStrategy(decimal teaPrice, decimal bedPrice)
        {
            TeaPrice = teaPrice;
            BedPrice = bedPrice;
        }
        public List<PriceComponent> CalculatePrice(Reservation ticket)
        {
            var components = new List<PriceComponent>();

            var placeComponent = new PriceComponent() { Name = "Main price" };
            placeComponent.Value = ticket.PlaceInRun.Place.Carriage.DefaultPrice * ticket.PlaceInRun.Place.PriceMultiplier;
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


            var teaComponent = new PriceComponent()
            {
                Name = "Tea",
                Value = TeaPrice
            };
            components.Add(teaComponent);

            if (ticket.PlaceInRun.Place.Carriage.Type != CarriageType.Sedentary)
            {
                var bedComponent = new PriceComponent()
                {
                    Name = "Bed",
                    Value = BedPrice
                };
                components.Add(bedComponent);
            }
            
            

            return components;
        }
    }
}