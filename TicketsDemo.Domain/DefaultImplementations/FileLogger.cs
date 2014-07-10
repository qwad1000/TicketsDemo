using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class FileLogger : ILogger
    {
        private string _dataFolder;

        public FileLogger(string dataFolder)
        {
            _dataFolder = dataFolder;
        }

        public void Log(string message, LogSeverity severity)
        {
            using (var fileStreamWriter = new StreamWriter(Path.Combine(_dataFolder, "log.txt"), true))
            {
                var wrtStr = String.Format("{0}[{1}]: {2}", severity, DateTime.Now, message);
                fileStreamWriter.WriteLine(wrtStr);
            }
            //throw new NotImplementedException();
        }
    }
}
