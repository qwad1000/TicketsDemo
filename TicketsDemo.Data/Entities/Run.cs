using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Data.Entities
{
    public class Run
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TrainId { get; set; }
        public Train Train { get; set; }
        public List<PlaceInRun> Places { get; set; }
    }
}
