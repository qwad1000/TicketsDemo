using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.Interfaces
{
    public interface ISchedule
    {
        List<DateTime> GetAvailableDatesForNewRun(int trainId, DateTime start, DateTime end);
        Run CreateRun(int trainId,DateTime date);
        void DeleteRun(int runId);
    }
}
