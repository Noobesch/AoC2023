using System.Collections.Generic;

public class Day10
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
        List<(int row, int column)> galaxyList = new List<(int row, int column)>();
        HashSet<int> galaxyRows = new HashSet<int>();
        HashSet<int> galaxyColumns = new HashSet<int>();

        int rows = 0;
        int columns = 0;

        using (StreamReader reader = new StreamReader(path))
        {
            int rowCounter = 0;
            while (!reader.EndOfStream)
            {
                var symbols = reader.ReadLine().ToCharArray();

                for (var symbolIndex = 0; symbolIndex < symbols.Length; symbolIndex++)
                {
                    var symbol = symbols[symbolIndex];

                    if (symbol == '#')
                    {
                        galaxyList.Add((rowCounter, symbolIndex));
                        galaxyRows.Add(rowCounter);
                        galaxyColumns.Add(symbolIndex);
                    }
                    columns = symbolIndex;
                }

                rowCounter++;
                rows = rowCounter;
            }

            Console.WriteLine("Done");

            int[] rowCosts = new int[rows];
            int[] columnCosts = new int[columns];

            for (var rowIndex = 0; rowIndex < rowCosts.Length; rowIndex++)
            {
                if (galaxyRows.Contains(rowIndex))
                {
                    rowCosts[rowIndex] = 1;
                }
                else
                {
                    rowCosts[rowIndex] = 2;
                }
            }

            for (var columnIndex = 0; columnIndex < columnCosts.Length; columnIndex++)
            {
                if (galaxyColumns.Contains(columnIndex))
                {
                    columnCosts[columnIndex] = 1;
                }
                else
                {
                    columnCosts[columnIndex] = 2;
                }
            }

            int solution1 = 0;

            for (var i = 0; i < galaxyList.Count; i++)
            {
                var firstCoords = galaxyList[i];
                for (var j = i + 1; j < galaxyList.Count; j++)
                {
                    var secondCoords = galaxyList[j];

                    int steps = 0;

                    int differenceRow = firstCoords.row - secondCoords.row;

                    if (differenceRow < 0)
                    {
                        steps += CalculateSteps(firstCoords.row, secondCoords.row, rowCosts);
                    }
                    else
                    {
                        steps += CalculateSteps(secondCoords.row, firstCoords.row, columnCosts);
                    }

                    int differenceColumn = firstCoords.column - secondCoords.column;

                    if (differenceColumn < 0)
                    {
                        steps += CalculateSteps(firstCoords.column, secondCoords.column, columnCosts);
                    }
                    else
                    {
                        steps += CalculateSteps(secondCoords.column, firstCoords.column, columnCosts);
                    }

                    solution1 += steps;
                }
            }
            Console.WriteLine($"Solution 1 is {solution1}");
        }
    }


    static void Part2(string path)
    {
        List<(int row, int column)> galaxyList = new List<(int row, int column)>();
        HashSet<int> galaxyRows = new HashSet<int>();
        HashSet<int> galaxyColumns = new HashSet<int>();

        int rows = 0;
        int columns = 0;

        using (StreamReader reader = new StreamReader(path))
        {
            int rowCounter = 0;
            while (!reader.EndOfStream)
            {
                var symbols = reader.ReadLine().ToCharArray();

                for (var symbolIndex = 0; symbolIndex < symbols.Length; symbolIndex++)
                {
                    var symbol = symbols[symbolIndex];

                    if (symbol == '#')
                    {
                        galaxyList.Add((rowCounter, symbolIndex));
                        galaxyRows.Add(rowCounter);
                        galaxyColumns.Add(symbolIndex);
                    }
                    columns = symbolIndex;
                }

                rowCounter++;
                rows = rowCounter;
            }

            Console.WriteLine("Done");

            int[] rowCosts = new int[rows];
            int[] columnCosts = new int[columns];

            for (var rowIndex = 0; rowIndex < rowCosts.Length; rowIndex++)
            {
                if (galaxyRows.Contains(rowIndex))
                {
                    rowCosts[rowIndex] = 1;
                }
                else
                {
                    rowCosts[rowIndex] = 1000000;
                }
            }

            for (var columnIndex = 0; columnIndex < columnCosts.Length; columnIndex++)
            {
                if (galaxyColumns.Contains(columnIndex))
                {
                    columnCosts[columnIndex] = 1;
                }
                else
                {
                    columnCosts[columnIndex] = 1000000;
                }
            }

            long solution2 = 0;

            for (var i = 0; i < galaxyList.Count; i++)
            {
                var firstCoords = galaxyList[i];
                for (var j = i + 1; j < galaxyList.Count; j++)
                {
                    var secondCoords = galaxyList[j];

                    int steps = 0;

                    int differenceRow = firstCoords.row - secondCoords.row;

                    if (differenceRow < 0)
                    {
                        steps += CalculateSteps(firstCoords.row, secondCoords.row, rowCosts);
                    }
                    else
                    {
                        steps += CalculateSteps(secondCoords.row, firstCoords.row, columnCosts);
                    }

                    int differenceColumn = firstCoords.column - secondCoords.column;

                    if (differenceColumn < 0)
                    {
                        steps += CalculateSteps(firstCoords.column, secondCoords.column, columnCosts);
                    }
                    else
                    {
                        steps += CalculateSteps(secondCoords.column, firstCoords.column, columnCosts);
                    }

                    solution2 += steps;
                }
            }
            Console.WriteLine($"Solution 2 is {solution2}");
        }
    }
    private static int CalculateSteps(int row1, int row2, int[] costArray)
    {
        int stepCounter = 0;
        for (int i = row1; i < row2; i++)
        {
            stepCounter += costArray[i];
        }

        return stepCounter;
    }
}