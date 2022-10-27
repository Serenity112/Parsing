using System;
using System.Xml;
using System.Collections.Generic;

namespace Parser
{
    class XmlParser
    {
        private HashSet<AddressData> addresses = new HashSet<AddressData>();
        public Dictionary<AddressData, int> addressCopies = new Dictionary<AddressData, int>();
        public Dictionary<string, FloorsData> cityFloorsData = new Dictionary<string, FloorsData>();
        public long elapsedTime = 0;
        public int linesCount = 0;

        public XmlParser(string path)
        {
            Parse(path);
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

        private void Parse(string path)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            using (XmlReader xml = XmlReader.Create(path))
            {
                while (xml.Read())
                {
                    switch (xml.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (xml.Name == "item")
                            {
                                AddressData data = new AddressData();

                                linesCount++;
                                if (xml.HasAttributes)
                                {
                                    while (xml.MoveToNextAttribute())
                                    {
                                        if (xml.Name == "city")
                                        {
                                            data.city = xml.Value;
                                            continue;
                                        }

                                        if (xml.Name == "street")
                                        {
                                            data.street = xml.Value;
                                            continue;
                                        }

                                        if (xml.Name == "house")
                                        {
                                            data.house = Int32.Parse(xml.Value);
                                            continue;
                                        }

                                        if (xml.Name == "floor")
                                        {
                                            data.floor = Int32.Parse(xml.Value);
                                            continue;
                                        }
                                    }
                                }

                                ProcessData(data);
                            }
                            break;
                    }
                }
            }


            watch.Stop();
            elapsedTime = watch.ElapsedMilliseconds;
        }

    }
}
