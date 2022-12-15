using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            newRequest();
        }

        static void newRequest()
        {
            Dictionary<AddressData, int> addressCopies = new Dictionary<AddressData, int>();
            Dictionary<string, FloorsData> cityFloorsData = new Dictionary<string, FloorsData>();
            long elapsedTime = 0;
            int linesCount = 0;


            Console.Write("Enter option. \n0 - Exit program\n1 - Enter file path\n");

            string input = Console.ReadLine();

            try
            {
                int result = Int32.Parse(input);

                switch (result)
                {
                    case 0:
                        break;
                    case 1:
                        Console.Write("File path: ");

                        string filePath = Console.ReadLine();

                        int lastDotIndex = -1;

                        for (int i = filePath.Length - 1; i >= 0; i--)
                        {
                            if (filePath[i] == '.')
                            {
                                lastDotIndex = i;
                                break;
                            }
                        }

                        if (lastDotIndex == -1)
                            throw new ArgumentException("File format not found!\n");

                        string extention = filePath.Substring(lastDotIndex + 1);
                        Console.WriteLine(extention);

                        switch (extention)
                        {
                            case "xml":
                                var xmlParser = new XmlParser(filePath);

                                elapsedTime = xmlParser.elapsedTime;
                                linesCount = xmlParser.linesCount;
                                addressCopies = xmlParser.addressCopies;
                                cityFloorsData = xmlParser.cityFloorsData;
                                break;
                            case "csv":
                                var csvParser = new CsvParser(filePath);

                                elapsedTime = csvParser.elapsedTime;
                                linesCount = csvParser.linesCount;
                                addressCopies = csvParser.addressCopies;
                                cityFloorsData = csvParser.cityFloorsData;
                                break;
                            default:
                                throw new ArgumentException("Invalid file format!\n");
                        }

                        Console.WriteLine($"Elapsed time: {elapsedTime}");
                        Console.WriteLine($"Number of records: {linesCount}");

                        Console.WriteLine();

                        Console.WriteLine("Copies of addresses:");
                        foreach (var copy in addressCopies)
                        {
                            Console.WriteLine(copy.Key);
                            Console.WriteLine($"Copy times: {copy.Value}");
                        }

                        Console.WriteLine();

                        Console.WriteLine("Floors data for particular cities:");
                        foreach (var data in cityFloorsData)
                        {
                            Console.WriteLine($"City: {data.Key}, floors: {data.Value}");
                        }


                        newRequest();
                        break;
                    default:
                        newRequest();
                        break;
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine($"Wrong input format: '{input}'\n");
                newRequest();
            }

            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                newRequest();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                newRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                newRequest();
            }
        }

    }
}
