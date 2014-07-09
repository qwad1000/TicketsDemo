using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class Train
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public List<Carriage> Carriages { get; set; }
    }
}
