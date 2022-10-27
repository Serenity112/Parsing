using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Parser
{
    class CsvParser
    {
        private HashSet<AddressData> addresses = new HashSet<AddressData>();
        public Dictionary<AddressData, int> addressCopies = new Dictionary<AddressData, int>();
        public Dictionary<string, FloorsData> cityFloorsData = new Dictionary<string, FloorsData>();
        public long elapsedTime = 0;
        public int linesCount = 0;

        public CsvParser(string path)
        {
            Parse(path);
        }
        private void Parse(string path)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();


            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                while (!parser.EndOfData)
                {
                    AddressData data = new AddressData();

                    string[] fields = new string[4];

                    fields = parser.ReadFields();

                    linesCount++;
                    try
                    {
                        data.city = fields[0];
                        data.street = fields[1];
                        data.house = Int32.Parse(fields[2]);
                        data.floor = Int32.Parse(fields[3]);

                        ProcessData(data);
                    }
                    catch { }

                }
            }

            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds;
        }

        private void ProcessData(AddressData data)
        {
            if (addresses.Contains(data))
            {
                if (addressCopies.ContainsKey(data))
                {
                    addressCopies[data]++;
                }
                else
                {
                    addressCopies.Add(data, 1);
                }
            }
            else
            {
                addresses.Add(data);

                if (cityFloorsData.ContainsKey(data.city))
                {
                    cityFloorsData[data.city].floors[data.floor]++;
                }
                else
                {
                    cityFloorsData.Add(data.city, new FloorsData());
                    cityFloorsData[data.city].floors[data.floor]++;
                }
            }
        }

    }


}
