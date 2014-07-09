using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    class Logger : ILogger
    {
        public void Log(string message, LogSeverity severity)
        {
            throw new NotImplementedException();
        }
    }
}
