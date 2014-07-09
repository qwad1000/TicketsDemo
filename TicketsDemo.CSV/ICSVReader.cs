using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV
{
    public interface ICSVReader
    {
        List<T> ReadFile<T>(string file); 
    }
}
