using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.CSV.CSVDTO;
using TicketsDemo.Data.Entities;
using TicketsDemo.Data.Repositories;

namespace TicketsDemo.CSV.Repositories
{
    public class CSVTrainRepository : ITrainRepository
    {
        private string _dataFolder;
        private ICSVReader _reader;

        public CSVTrainRepository(string dataFolder, ICSVReader csvReader)
        {
            _reader = csvReader;
            _dataFolder = dataFolder;
        }

        #region ITrainRepository Members

        public List<Train> GetAllTrains()
        {
            var trainsDTO = _reader.ReadFile<TrainCSV>(Path.Combine(_dataFolder,"trains.txt"));

            return trainsDTO.Select(td => new Train()
            {
                Carriages = new List<Carriage>(),
                StartLocation = td.StartLocation,
                EndLocation = td.EndLocation,
                Number = int.Parse(td.Number),
                Id = int.Parse(td.Number)
            }).ToList();
        }

        public Data.Entities.Train GetTrainDetails(int id)
        {
            var train = GetAllTrains().Single(t => t.Id == id);
            train.Carriages = new List<Carriage>();

            var i = 0;
            foreach(var carriageDTO in _reader.ReadFile<CarriageCSV>(Path.Combine(_dataFolder, String.Format("carriage{0}.txt",id)))){
                var carr = new Carriage()
                {
                    TrainId = id,
                    Train = train,
                    DefaultPrice = decimal.Parse(carriageDTO.Price),
                    Id = i,
                    Type = (CarriageType)Enum.Parse(typeof(CarriageType), carriageDTO.Type),
                    Places = new List<Place>()
                };

                var places = int.Parse(carriageDTO.Places);
                for (int pl = 1; pl <= places; pl++) {
                    var newPlace = new Place() { 
                        Carriage = carr, 
                        CarriageId = carr.Id, 
                        Number = pl,
                        Id = carr.Id*1000000 + pl, //magic
                        PriceMultiplier = 1 
                    };
                    carr.Places.Add(newPlace);
                }

                i++;
                train.Carriages.Add(carr);
            }

            return train;
        }

        public void CreateTrain(Data.Entities.Train train)
        {
            throw new NotImplementedException();
        }

        public void UpdateTrain(Data.Entities.Train train)
        {
            throw new NotImplementedException();
        }

        public void DeleteTrain(Data.Entities.Train train)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
