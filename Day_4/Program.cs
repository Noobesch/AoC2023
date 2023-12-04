
public class Day4
{
    static void Main()
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
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

                solution1 += lineSolution;
            }
            Console.WriteLine($"Solution 1 is {solution1}");
        }
    }

    static void Part2()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            List<List<int>> winningList = new List<List<int>>();
            List<List<int>> ownedList = new List<List<int>>();

            string line;
            int firstColonIndex;

            int solution2 = 0;

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

                winningList.Add(winningNumbersInLine);
                ownedList.Add(ownedNumbersInLine);
            }


            int[] cardAmountArray = new int[winningList.Count];

            for (var i = 0; i < cardAmountArray.Length; i++)
            {
                cardAmountArray[i] = 1;
            }

            //Loop every line and find matches and then add to card amount array
            for (var lineIndex = 0; lineIndex < cardAmountArray.Length; lineIndex++)
            {
                var winners = winningList[lineIndex];
                var ownedNumbers = ownedList[lineIndex];

                int matches = 0;

                foreach (var ownedNumber in ownedNumbers)
                {
                    if (winners.Contains(ownedNumber))
                    {
                        matches++;
                    }
                }

                for (var cards = 0; cards < cardAmountArray[lineIndex]; cards++)
                {
                    for (var additionalCards = 0; additionalCards < matches; additionalCards++)
                    {
                        var index = lineIndex + 1 + additionalCards;

                        if(index <= cardAmountArray.Length)
                        {
                            cardAmountArray[lineIndex + 1 + additionalCards]++;
                        }
                    }
                }
            }

            for(var solutionIndex = 0; solutionIndex < cardAmountArray.Length; solutionIndex++)
            {
                solution2 += cardAmountArray[solutionIndex];
            }

            Console.WriteLine($"Solution 2 is {solution2}");
        }
    }
}