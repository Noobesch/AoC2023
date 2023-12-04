
public class Day4
{
    static void Main()
    {
        Part1();
    }

    static void Part1()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            List<string> lines = new List<string>();

            string line;
            int firstColonIndex;

            int solution1 = 0;

            while (!reader.EndOfStream)
            {
                int lineSolution = 0;

                line = reader.ReadLine().Replace("\r\n", "");
                firstColonIndex = line.IndexOf(":");
                line = line.Remove(0, firstColonIndex + 1);

                var seperatedNumber = line.Split('|');

                var winningNumbers = seperatedNumber[0].Split(' ');
                var ownedNumbers = seperatedNumber[1].Split(' ');

                List<int> winningNumbersInLine = new List<int>();
                List<int> ownedNumbersInLine = new List<int>();

                foreach (var winningNumber in winningNumbers)
                {
                    if (int.TryParse(winningNumber, out int parsedNumber))
                    {
                        winningNumbersInLine.Add(parsedNumber);
                    }
                }

                foreach (var ownedNumber in ownedNumbers)
                {
                    if (int.TryParse(ownedNumber, out int parsedNumber))
                    {
                        ownedNumbersInLine.Add(parsedNumber);
                    }
                }

                foreach (var ownedNumber in ownedNumbersInLine)
                {
                    if (winningNumbersInLine.Contains(ownedNumber))
                    {
                        if (lineSolution == 0)
                        {
                            lineSolution = 1;
                        }
                        else
                        {
                            lineSolution *= 2;
                        }
                    }
                }

                Console.WriteLine($"Line solution is {lineSolution}");
                solution1 += lineSolution;
            }
            Console.WriteLine(solution1);
        }
    }
}