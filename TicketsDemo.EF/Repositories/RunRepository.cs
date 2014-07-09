using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.EF.Repositories
{
    public class RunRepository : IRunRepository
    {
        ITrainRepository _trainRepo;

        public RunRepository(ITrainRepository trainRepo)
        {
            _trainRepo = trainRepo;
        }

        #region IRunRepository Members

        public void CreateRun(Data.Entities.Run run)
        {
            using (var ctx = new TicketsContext())
            {
                if (run.Train != null) //технический момент - указание не пересоздавать train
                {
                    ctx.Trains.Attach(run.Train);
                    ctx.Entry(run.Train).State = System.Data.Entity.EntityState.Unchanged;
                }

                ctx.Runs.Add(run);
                ctx.SaveChanges();
            }
        }

        public void DeleteRun(Data.Entities.Run runId)
        {
            using (var ctx = new TicketsContext())
            {
                var run = ctx.Runs.Find(runId);
                ctx.Runs.Remove(run);
                ctx.SaveChanges();
            }
        }

        public List<Data.Entities.Run> GetRuns(DateTime start, DateTime end, int? traintId = null)
        {
            using (var ctx = new TicketsContext())
            {
                return ctx.Runs.Where(r => r.Date > start && r.Date < end && (traintId == null || r.TrainId == traintId)).ToList();
            }
        }

        public Data.Entities.Run GetRunDetails(int runId)
        {
            using (var ctx = new TicketsContext())
            {
                var run = ctx.Runs.Include("Places")
                    //.Include("Places.Place")
                    .Include("Places.Reservations")
                    .Include("Places.Reservations.Ticket")
                    //.Include("Train")
                    //.Include("Train.Carriages")
                    .Where(r => r.Id == runId).Single();

                var train = _trainRepo.GetTrainDetails(run.TrainId);
                run.Train = train;

                var placeIdToPlace = new Dictionary<int, Place>();
                foreach (var car in train.Carriages)
                {
                    foreach (var plc in car.Places)
                    {
                        placeIdToPlace.Add(plc.Id, plc);
                    }
                }

                foreach (var plc in run.Places) {
                    plc.Place = placeIdToPlace[plc.PlaceId];
                } 
                
                return run;
            }
        }

        #endregion
    }
}
