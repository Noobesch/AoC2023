
public class Day2
{
    static void Main()
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        string path = "Input_1.txt";
            //path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            List<string> lines = new List<string>();
            string line;

            int solution1 = 0;
            int lineCount = 1;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                var firstColonIndex = line.IndexOf(':');
                line = line.Remove(0, firstColonIndex + 1);

                bool isValid = true;

                //line now contains only sets

                var gamesInLine = line.Split(';');

                foreach (var game in gamesInLine)
                {
                    var blueIndex = game.IndexOf("blue");
                    var redIndex = game.IndexOf("red");
                    var greenIndex = game.IndexOf("green");

                    int blueCount = 0;
                    int redCount = 0;
                    int greenCount = 0;

                    if (blueIndex != -1)
                    {
                        var number = game.Substring(blueIndex - 3, 3);
                        blueCount = int.Parse(number);
                    }


                    if (redIndex != -1)
                    {
                        var number = game.Substring(redIndex - 3, 3);
                        redCount = int.Parse(number);
                    }

                    if (greenIndex != -1)
                    {
                        var number = game.Substring(greenIndex - 3, 3);
                        greenCount = int.Parse(number);
                    }

                    if (blueCount > 14 || greenCount > 13 || redCount > 12)
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    solution1 += lineCount;
                }

                lineCount++;
            }

            Console.WriteLine($"Solution 1 = {solution1}");
        }
    }

    static void Part2()
    {
        string path = "Input_1.txt";
            // path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            List<string> lines = new List<string>();
            string line;

            int solution2 = 0;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                var firstColonIndex = line.IndexOf(':');
                line = line.Remove(0, firstColonIndex + 1);

                //line now contains only sets

                int lowestRed = int.MinValue;
                int lowestBlue = int.MinValue;
                int lowestGreen = int.MinValue;

                var gamesInLine = line.Split(';');

                foreach (var game in gamesInLine)
                {
                    var blueIndex = game.IndexOf("blue");
                    var redIndex = game.IndexOf("red");
                    var greenIndex = game.IndexOf("green");

                    int blueCount = 0;
                    int redCount = 0;
                    int greenCount = 0;

                    if (blueIndex != -1)
                    {
                        var number = game.Substring(blueIndex - 3, 3);
                        blueCount = int.Parse(number);

                        if (blueCount > lowestBlue)
                        {
                            lowestBlue = blueCount;
                        }
                    }


                    if (redIndex != -1)
                    {
                        var number = game.Substring(redIndex - 3, 3);
                        redCount = int.Parse(number);

                        if (redCount > lowestRed)
                        {
                            lowestRed = redCount;
                        }
                    }

                    if (greenIndex != -1)
                    {
                        var number = game.Substring(greenIndex - 3, 3);
                        greenCount = int.Parse(number);

                        if (greenCount > lowestGreen)
                        {
                            lowestGreen = greenCount;
                        }
                    }
                }


                solution2 += (lowestBlue * lowestGreen * lowestRed);
            }

            Console.WriteLine($"Solution 2 = {solution2}");
        }
    }
}