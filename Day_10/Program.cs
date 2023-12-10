public class Day10
{


    static void Main()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
        Part1(path);
        // Part2(path);
    }

    static void Part1(string path)
    {
        Dictionary<char, (int row, int column)[]> mappingDict = new Dictionary<char, (int row, int column)[]>();

        mappingDict.Add('|', new (int row, int column)[] { (-1, 0), (1, 0) });
        mappingDict.Add('-', new (int row, int column)[] { (0, 1), (0, -1) });
        mappingDict.Add('L', new (int row, int column)[] { (-1, 0), (0, 1) });
        mappingDict.Add('J', new (int row, int column)[] { (-1, 0), (0, -1) });
        mappingDict.Add('7', new (int row, int column)[] { (1, 0), (0, -1) });
        mappingDict.Add('F', new (int row, int column)[] { (1, 0), (0, 1) });

        using (StreamReader reader = new StreamReader(path))
        {
            List<char[]> inputList = new List<char[]>();

            (int row, int column) startPosition = (-1, -1);
            int rowCounter = 0;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\r\n", "");

                if (line.Contains('S'))
                {
                    startPosition = (rowCounter, line.IndexOf('S'));
                }
                inputList.Add(line.ToCharArray());

                rowCounter++;
            }

            Console.WriteLine($"Done, S is at {startPosition.row}, {startPosition.column}");

            int stepCounter = 0;

            (int row, int column)[] cardinalDirections = { (-1, 0), (0, -1), (0, 1), (1, 0) };

            for (var directionIndex = 0; directionIndex < cardinalDirections.Length; directionIndex++)
            {
                var nextStep = cardinalDirections[directionIndex];
                (int row, int column) lastPosition = (startPosition.row, startPosition.column);
                (int row, int column) currentPosition = (startPosition.row + nextStep.row, startPosition.column + nextStep.column);

                while (true)
                {
                    var symbol = inputList[currentPosition.row][currentPosition.column];

                    if (symbol == 'S')
                    {
                        Console.WriteLine("Found the loop");
                        break;
                    }

                    var possibleNextSteps = mappingDict[symbol];
                    bool backPossible = false;
                    bool forwardPossible = false;

                    foreach (var step in possibleNextSteps)
                    {
                        (int row, int column) tempNext = (currentPosition.row + step.row, currentPosition.column + step.column);



                        if (tempNext.row < 0 || tempNext.row >= inputList.Count ||
                        tempNext.column < 0 || tempNext.column >= inputList[0].Length)
                        {
                            break;
                        }

                        var tempNextSymbol = inputList[tempNext.row][tempNext.column];
                        var tempNextSteps = mappingDict[tempNextSymbol];

                        if (tempNext.row != lastPosition.row || tempNext.column != lastPosition.column)
                        {
                            forwardPossible = true;

                            (int row, int column) backStep = (-step.row, -step.column);
                            if (tempNextSteps.Contains(backStep))
                            {
                                backPossible = true;
                            }

                            nextStep = tempNext;
                        }
                    }

                    if (backPossible && forwardPossible)
                    {
                        lastPosition = currentPosition;
                        currentPosition = nextStep;
                        stepCounter++;
                    }
                    else
                    {
                        Console.WriteLine("This start direction was not possible");
                        break;
                    }
                }
            }


        }
    }
}