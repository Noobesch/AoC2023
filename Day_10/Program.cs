public class Day10
{
    static void Main()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
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

            int rowLength = inputList.Count;
            int columnLength = inputList[0].Length;

            for (var runThroughs = 0; runThroughs < 5; runThroughs++)
            {
                for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
                {
                    for (var columnIndex = 0; columnIndex < inputList[0].Length; columnIndex++)
                    {
                        char symbol = inputList[rowIndex][columnIndex];

                        if (!mappingDict.Keys.Contains(symbol))
                        {
                            continue;
                        }

                        foreach (var step in mappingDict[symbol])
                        {
                            (int row, int column) nextCoords = (rowIndex + step.row, columnIndex + step.column);

                            if (nextCoords.row < 0 || nextCoords.row >= rowLength ||
                            nextCoords.column < 0 || nextCoords.column >= columnLength)
                            {
                                inputList[rowIndex][columnIndex] = '.';
                                continue;
                            }


                            char nextSymbol = inputList[rowIndex + step.row][columnIndex + step.column];

                            if (nextSymbol == '.')
                            {
                                inputList[rowIndex][columnIndex] = '.';
                                continue;
                            }
                            else if (nextSymbol == 'S')
                            {
                                continue;
                            }

                            (int row, int column)[] steps = mappingDict[nextSymbol];

                            if (!steps.Contains((-step.row, -step.column)))
                            {
                                inputList[rowIndex][columnIndex] = '.';
                            }
                        }
                    }
                }
            }


            int loopLength = 0;

            for (var lineIndex = 0; lineIndex < inputList.Count; lineIndex++)
            {
                var line = inputList[lineIndex];

                for (var charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    char character = line[charIndex];
                    loopLength++;
                    switch (character)
                    {
                        case '|':
                            line[charIndex] = '|';
                            break;
                        case '-':
                            line[charIndex] = '─';
                            break;
                        case 'L':
                            line[charIndex] = '└';
                            break;
                        case 'J':
                            line[charIndex] = '┘';
                            break;
                        case '7':
                            line[charIndex] = '┐';
                            break;
                        case 'F':
                            line[charIndex] = '┌';
                            break;
                        default:
                            loopLength--;
                            break;
                    }
                }

                inputList[lineIndex] = line;
            }

            for (var lineIndex = 0; lineIndex < inputList.Count; lineIndex++)
            {
                for (var charIndex = 0; charIndex < inputList[0].Length; charIndex++)
                {
                    Console.Write(inputList[lineIndex][charIndex]);
                }
                Console.WriteLine();
            }

            Console.WriteLine($"Loop length is {(loopLength + 1) / 2}");
        }
    }
}