using System.Diagnostics;
using System.Security.AccessControl;

public class Day6
{
    static void Main()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
        Part1(path);
    }

    static void Part1(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            var times = reader.ReadLine().Split(' ',StringSplitOptions.RemoveEmptyEntries);
            var distances = reader.ReadLine().Split(' ',StringSplitOptions.RemoveEmptyEntries);

            long solution1 = 1;

            for(var i = 1; i < times.Length; i++)
            {
                var time = long.Parse(times[i]);
                var distance = long.Parse(distances[i]);

                long numberOfSolutions = 0;

                for(var timeIndex = time; timeIndex > 0; timeIndex--)
                {
                    long speed = time - timeIndex;

                    if((speed * timeIndex) > distance)
                    {
                        numberOfSolutions++;
                    }
                }

                solution1 *= numberOfSolutions;
            }

            Console.WriteLine($"The solution is {solution1}");

        }
    }
    static void Part2(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
        }
    }
}
