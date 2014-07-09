using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.Domain.Interfaces
{
    public enum LogSeverity{Debug,Info,Warning,Error,Fatal}

    public interface ILogger
    {
        void Log(string message,LogSeverity severity);
    }
}
