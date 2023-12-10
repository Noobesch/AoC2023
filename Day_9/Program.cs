public class Day9
{
    static void Main()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
        Part1(path);
        Part2(path);
    }

    static void Part1(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            List<List<int>> inputs = new List<List<int>>();

            while (!reader.EndOfStream)
            {
                string[] stringValues = reader.ReadLine().Replace("\r\n", "").Split(" ", StringSplitOptions.RemoveEmptyEntries);
                List<int> lineInputs = new List<int>();

                foreach (var value in stringValues)
                {
                    lineInputs.Add(int.Parse(value));
                }

                inputs.Add(lineInputs);
            }

            int solution1 = 0;

            foreach (var input in inputs)
            {
                List<List<int>> differenceLists = new List<List<int>>();
                differenceLists.Add(input);

                bool allZeros = false;
                var currentList = input;
                while (!allZeros)
                {
                    var newList = GenerateDifferenceList(currentList, out allZeros);
                    differenceLists.Add(newList);
                    currentList = newList;
                }

                differenceLists.Reverse();

                for (var listIndex = 0; listIndex < differenceLists.Count - 1; listIndex++)
                {
                    var currentLastVal = differenceLists[listIndex].LastOrDefault();
                    var nextLastVal = differenceLists[listIndex + 1].LastOrDefault();
                    differenceLists[listIndex + 1].Add(currentLastVal + nextLastVal);
                }

                int lastValueOfLine = differenceLists.LastOrDefault().LastOrDefault();

                solution1 += lastValueOfLine;

                Console.WriteLine($"Just added {lastValueOfLine}");
            }
            Console.WriteLine($"Solution 1 is {solution1}");

        }
    }


    static void Part2(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            List<List<int>> inputs = new List<List<int>>();

            while (!reader.EndOfStream)
            {
                string[] stringValues = reader.ReadLine().Replace("\r\n", "").Split(" ", StringSplitOptions.RemoveEmptyEntries);
                List<int> lineInputs = new List<int>();

                foreach (var value in stringValues)
                {
                    lineInputs.Add(int.Parse(value));
                }
                lineInputs.Reverse();
                inputs.Add(lineInputs);
            }

            int solution2 = 0;

            foreach (var input in inputs)
            {
                List<List<int>> differenceLists = new List<List<int>>();
                differenceLists.Add(input);

                bool allZeros = false;
                var currentList = input;
                while (!allZeros)
                {
                    var newList = GenerateDifferenceList(currentList, out allZeros);
                    differenceLists.Add(newList);
                    currentList = newList;
                }

                differenceLists.Reverse();

                for (var listIndex = 0; listIndex < differenceLists.Count - 1; listIndex++)
                {
                    var currentLastVal = differenceLists[listIndex].LastOrDefault();
                    var nextLastVal = differenceLists[listIndex + 1].LastOrDefault();
                    differenceLists[listIndex + 1].Add(currentLastVal + nextLastVal);
                }

                int lastValueOfLine = differenceLists.LastOrDefault().LastOrDefault();

                solution2 += lastValueOfLine;

                Console.WriteLine($"Just added {lastValueOfLine}");
            }
            Console.WriteLine($"Solution 2 is {solution2}");

        }
    }
    static List<int> GenerateDifferenceList(List<int> inputList, out bool allZeros)
    {
        List<int> returnList = new List<int>();
        allZeros = true;

        for (var i = 0; i < inputList.Count - 1; i++)
        {
            int newVal = inputList[i + 1] - inputList[i];
            returnList.Add(newVal);

            if (newVal != 0)
            {
                allZeros = false;
            }
        }

        return returnList;
    }
}