using System.Diagnostics;
using System.Security.AccessControl;

public class Day5
{
    static void Main()
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string input = reader.ReadToEnd();


            var departments = input.Split("\r\n\r\n");

            for (var i = 0; i < departments.Length; i++)
            {
                var department = departments[i];

                var firstColon = department.IndexOf(":");
                department = department.Remove(0, firstColon + 1);

                departments[i] = department;
            }

            List<long> seeds = new List<long>();

            var stringSeed = departments[0].Split(" ");

            foreach (var seed in stringSeed)
            {
                if (seed == "")
                {
                    continue;
                }
                seeds.Add(long.Parse(seed));
            }


            List<List<(long destination, long source, long length)>> completeList = new List<List<(long source, long destination, long length)>>();

            for (var lines = 1; lines < departments.Length; lines++)
            {
                var department = departments[lines];

                //Values per line
                var values = department.Split("\r\n");

                List<(long destination, long source, long length)> departmentValues = new List<(long source, long destination, long length)>();

                foreach (var value in values)
                {

                    if (value == "")
                    {
                        continue;
                    }
                    var splitValues = value.Split(" ");

                    departmentValues.Add((long.Parse(splitValues[0]), long.Parse(splitValues[1]), long.Parse(splitValues[2])));
                }

                completeList.Add(departmentValues);
            }

            long smallestLocation = long.MaxValue;

            for (var seedIndex = 0; seedIndex < seeds.Count; seedIndex++)
            {
                var seed = seeds[seedIndex];

                for (var completeIndex = 0; completeIndex < completeList.Count; completeIndex++)
                {
                    var department = completeList[completeIndex];

                    for (var departmentIndex = 0; departmentIndex < department.Count; departmentIndex++)
                    {
                        var valueTuple = department[departmentIndex];
                        if (seed >= valueTuple.source && seed < (valueTuple.source + valueTuple.length))
                        {
                            var difference = valueTuple.destination - valueTuple.source;
                            seed += difference;
                            break;
                        }
                    }
                }
                if (seed < smallestLocation)
                {
                    smallestLocation = seed;
                }
            }

            Console.WriteLine(smallestLocation);
        }
    }

    static void Part2()
    {
        Stopwatch sw = new Stopwatch();

        sw.Start();
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string input = reader.ReadToEnd();


            var departments = input.Split("\r\n\r\n");

            for (var i = 0; i < departments.Length; i++)
            {
                var department = departments[i];

                var firstColon = department.IndexOf(":");
                department = department.Remove(0, firstColon + 1);

                departments[i] = department;
            }

            List<long> seeds = new List<long>();

            var stringSeed = departments[0].Split(" ");

            for (var seedIndex = 0; seedIndex < stringSeed.Length; seedIndex++)
            {
                if (stringSeed[seedIndex] == "")
                {
                    continue;
                }

                seeds.Add(long.Parse(stringSeed[seedIndex]));
            }

            List<List<(long destination, long source, long length)>> completeList = new List<List<(long source, long destination, long length)>>();

            for (var lines = 1; lines < departments.Length; lines++)
            {
                var department = departments[lines];

                //Values per line
                var values = department.Split("\r\n");

                List<(long destination, long source, long length)> departmentValues = new List<(long source, long destination, long length)>();

                foreach (var value in values)
                {

                    if (value == "")
                    {
                        continue;
                    }
                    var splitValues = value.Split(" ");

                    departmentValues.Add((long.Parse(splitValues[0]), long.Parse(splitValues[1]), long.Parse(splitValues[2])));
                }

                completeList.Add(departmentValues);
            }

            long possibleLocation = 0;
            bool locationFound = false;

            while (!locationFound)
            {
                long localPossibleLocation = possibleLocation;
                for (var completeIndex = completeList.Count - 1; completeIndex >= 0; completeIndex--)
                {
                    var department = completeList[completeIndex];

                    for (var departmentIndex = 0; departmentIndex < department.Count; departmentIndex++)
                    {
                        var valueTuple = department[departmentIndex];

                        if (localPossibleLocation >= valueTuple.destination && localPossibleLocation < (valueTuple.destination + valueTuple.length))
                        {
                            var difference = valueTuple.source - valueTuple.destination;
                            localPossibleLocation += difference;
                            break;
                        }
                    }
                }

                for (var seedIndex = 0; seedIndex < seeds.Count; seedIndex += 2)
                {
                    var start = seeds[seedIndex];
                    var end = start + seeds[seedIndex + 1];

                    if (localPossibleLocation >= start && localPossibleLocation <= end)
                    {
                        locationFound = true;
                        Console.WriteLine($"Found location at {possibleLocation} in {sw.ElapsedMilliseconds} ms");
                        break;
                    }
                }

                possibleLocation++;
            }
        }
    }
}