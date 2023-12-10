public class Day8
{
    static void Main()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        Part1(path);
        Part2(path);
    }

    static void Part1(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string instructions = reader.ReadLine();
            reader.ReadLine();

            List<(string left, string right)> directions = new List<(string left, string right)>();
            Dictionary<string, (string left, string right)> inputDict = new Dictionary<string, (string left, string right)>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                line = line.Replace("(", "").Replace(")", "").Replace("=", "").Replace(",", "");

                var inputs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                inputDict.Add(inputs[0], (inputs[1], inputs[2]));
            }

            int solution1Counter = 0;
            string currentInput = "AAA";

            while (true)
            {
                var nextStep = instructions[solution1Counter % instructions.Length];
                var nextInputs = inputDict[currentInput];

                solution1Counter++;

                currentInput = (nextStep == 'L') ? nextInputs.left : nextInputs.right;

                if (currentInput == "ZZZ")
                {
                    break;
                }
            }

            Console.WriteLine($"Solution 1 is reached after {solution1Counter} steps");
        }
    }

    static void Part2(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            string instructions = reader.ReadLine();
            reader.ReadLine();

            List<(string left, string right)> directions = new List<(string left, string right)>();
            Dictionary<string, (string left, string right)> inputDict = new Dictionary<string, (string left, string right)>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                line = line.Replace("(", "").Replace(")", "").Replace("=", "").Replace(",", "");

                var inputs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                inputDict.Add(inputs[0], (inputs[1], inputs[2]));
            }

            int solution2Counter = 0;

            List<string> currentInputs = new List<string>();
            foreach (var input in inputDict.Keys)
            {
                if (input.EndsWith('A'))
                {
                    currentInputs.Add(input);
                }
            }

            int[] offsetts = new int[currentInputs.Count];
            int[] loopLengths = new int[currentInputs.Count];
            bool[] firstFinishFound = new bool[currentInputs.Count];
            bool[] loopLengthFound = new bool[currentInputs.Count];

            while (true)
            {
                var nextStep = instructions[solution2Counter % instructions.Length];
                solution2Counter++;

                bool isFinished = true;

                for (var inputIndex = 0; inputIndex < currentInputs.Count; inputIndex++)
                {
                    var currentInput = currentInputs[inputIndex];
                    var nextInputs = inputDict[currentInput];

                    currentInput = (nextStep == 'L') ? nextInputs.left : nextInputs.right;

                    currentInputs[inputIndex] = currentInput;

                    if (!currentInput.EndsWith('Z'))
                    {
                        isFinished = false;
                    }
                    else
                    {
                        if (!firstFinishFound[inputIndex])
                        {
                            offsetts[inputIndex] = solution2Counter;
                            firstFinishFound[inputIndex] = true;
                        }
                        else if (!loopLengthFound[inputIndex])
                        {
                            loopLengths[inputIndex] = solution2Counter - offsetts[inputIndex];
                            loopLengthFound[inputIndex] = true;
                        }
                    }

                    if (!loopLengthFound.Contains(false))
                    {
                        isFinished = true;
                        break;
                    }
                }

                if (isFinished)
                {
                    break;
                }
            }

            var loopLengthList = loopLengths.ToList();
            loopLengthList.Sort();

            int biggestMember = loopLengthList[loopLengthList.Count - 1];

            for (long i = 1; i < 10000000000000; i++)
            {
                long potentialSolution = biggestMember * i;

                bool found = true;
                foreach (var loopLength in loopLengthList)
                {
                    if (potentialSolution % loopLength != 0)
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    Console.WriteLine($"Solution 2 is reached after {potentialSolution} steps");
                    break;
                }
            }

            Console.WriteLine("Did not find solution 2");

        }
    }
}
