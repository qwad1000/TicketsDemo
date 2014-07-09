using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketsDemo.CSV
{
    public class CSVReader : ICSVReader
    {
        public List<T> ReadFile<T>(string file) {
            using (var reader = new StreamReader(File.OpenRead(file)))
            {

                List<T> retIt = new List<T>();

                Dictionary<int, string> titles = null;

                var allProperties = typeof(T).GetProperties().ToDictionary(p => p.Name);

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var valuesArray = line.Split(';');
                    if (titles == null)
                    {
                        titles = new Dictionary<int, string>();
                        for (int i = 0; i < valuesArray.Length; i++)
                        {
                            titles.Add(i, valuesArray[i]);
                        };
                    }
                    else
                    {
                        T obj = Activator.CreateInstance<T>();
                        for (int i = 0; i < valuesArray.Length; i++)
                        {
                            var value = valuesArray[i];
                            var propName = titles[i];
                            if (allProperties.ContainsKey(propName))
                            {
                                var prop = allProperties[propName];
                                prop.SetValue(obj, value);
                            }
                        };
                        retIt.Add(obj);
                    }
                }

                return retIt;
            }
        }
    }
}
