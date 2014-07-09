using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class Place
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal PriceMultiplier { get; set; }
        public int CarriageId { get; set; }
        public Carriage Carriage { get; set; }
    }
}
