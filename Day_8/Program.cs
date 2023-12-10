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

            int solution1Counter = 0;

            List<string> currentInputs = new List<string>();

            foreach (var input in inputDict.Keys)
            {
                if (input.EndsWith('A'))
                {
                    currentInputs.Add(input);
                }
            }

            while (true)
            {
                var nextStep = instructions[solution1Counter % instructions.Length];

                for (var inputIndex = 0; inputIndex < currentInputs.Count; inputIndex++)
                {

                    var nextInputs = inputDict[currentInput];

                    solution1Counter++;

                    currentInput = (nextStep == 'L') ? nextInputs.left : nextInputs.right;

                    if (currentInput == "ZZZ")
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"Solution 1 is reached after {solution1Counter} steps");
        }
    }
}
