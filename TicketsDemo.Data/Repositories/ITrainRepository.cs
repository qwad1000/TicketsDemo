using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Data.Repositories
{
    public interface ITrainRepository
    {
        List<Train> GetAllTrains();
        Train GetTrainDetails(int trainId);
        void CreateTrain(Train train);
        void UpdateTrain(Train train);
        void DeleteTrain(Train train);
    }
}
