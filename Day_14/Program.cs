using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class Day14
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
        List<List<char>> inputList = new List<List<char>>();
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\r\n", "");
                List<char> linelist = line.ToCharArray().ToList();
                inputList.Add(linelist);
            }

            for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < inputList[rowIndex].Count; columnIndex++)
                {
                    char symbol = inputList[rowIndex][columnIndex];

                    if (symbol == '.' || symbol == '#')
                    {
                        continue;
                    }

                    int tempRowIndex = rowIndex - 1;
                    int lastValidRow = int.MaxValue;
                    while (tempRowIndex >= 0)
                    {
                        char tempSymbol = inputList[tempRowIndex][columnIndex];

                        if (tempSymbol == '.')
                        {
                            lastValidRow = tempRowIndex;
                        }

                        else
                        {
                            if (lastValidRow == int.MaxValue)
                            {
                                break;
                            }
                        }


                        if (lastValidRow != int.MaxValue &&
                        (tempSymbol != '.' || tempRowIndex <= 0))
                        {
                            inputList[lastValidRow][columnIndex] = 'O';
                            inputList[rowIndex][columnIndex] = '.';
                            break;
                        }
                        tempRowIndex--;
                    }
                }
            }

            long solution1 = 0;
            for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
            {
                int value = inputList.Count;
                for (var columnIndex = 0; columnIndex < inputList[rowIndex].Count; columnIndex++)
                {
                    char symbol = inputList[rowIndex][columnIndex];
                    if (symbol == 'O')
                    {
                        solution1 += inputList.Count - rowIndex;
                    }
                }
            }
            Console.WriteLine($"Solution 1 is {solution1}");
        }
    }

    static void Part2(string path)
    {
        List<List<char>> inputList = new List<List<char>>();
        HashSet<string> uniqueSets = new HashSet<string>();
        List<(string input, int value)> tupleList = new List<(string input, int value)>();

        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\r\n", "");
                List<char> linelist = line.ToCharArray().ToList();
                inputList.Add(linelist);
            }

            string firstRepetition = "";
            int firstRepetitionIndex = 0;
            int lastRepetitionIndex = 0;
            int cycleLength = 0;
            for (var repetition = 0; repetition < 1000000000; repetition++)
            {
                for (var i = 0; i < 4; i++)
                {
                    Tilter(ref inputList);
                    Rotator(ref inputList);
                }

                string stringifiedList = "";
                foreach (var input in inputList)
                {
                    stringifiedList += new string(input.ToArray());
                }

                if (!uniqueSets.Contains(stringifiedList))
                {
                    uniqueSets.Add(stringifiedList);

                    int lineSolution = 0;
                    for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
                    {
                        int value = inputList.Count;
                        for (var columnIndex = 0; columnIndex < inputList[rowIndex].Count; columnIndex++)
                        {
                            char symbol = inputList[rowIndex][columnIndex];
                            if (symbol == 'O')
                            {
                                lineSolution += inputList.Count - rowIndex;
                            }
                        }
                    }

                    tupleList.Add((stringifiedList, lineSolution));
                }
                else
                {
                    if (firstRepetitionIndex == 0)
                    {
                        firstRepetitionIndex = repetition;
                        firstRepetition = stringifiedList;
                        lastRepetitionIndex = repetition;
                    }
                    else if (stringifiedList == firstRepetition)
                    {
                        if (cycleLength == 0)
                        {
                            cycleLength = repetition - firstRepetitionIndex;
                        }
                        else if (cycleLength != (repetition - lastRepetitionIndex))
                        {
                            throw new Exception();
                        }
                        lastRepetitionIndex = repetition;
                        break;
                    }
                }
            }

            int remainder = (1000000000 - firstRepetitionIndex) % cycleLength;
            long solution2 = tupleList[remainder].value;
           
            Console.WriteLine($"Solution 2 is {solution2}");
        }
    }

    static void Tilter(ref List<List<char>> inputList)
    {
        for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < inputList[rowIndex].Count; columnIndex++)
            {
                char symbol = inputList[rowIndex][columnIndex];

                if (symbol == '.' || symbol == '#')
                {
                    continue;
                }

                int tempRowIndex = rowIndex - 1;
                int lastValidRow = int.MaxValue;
                while (tempRowIndex >= 0)
                {
                    char tempSymbol = inputList[tempRowIndex][columnIndex];

                    if (tempSymbol == '.')
                    {
                        lastValidRow = tempRowIndex;
                    }

                    else
                    {
                        if (lastValidRow == int.MaxValue)
                        {
                            break;
                        }
                    }


                    if (lastValidRow != int.MaxValue &&
                    (tempSymbol != '.' || tempRowIndex <= 0))
                    {
                        inputList[lastValidRow][columnIndex] = 'O';
                        inputList[rowIndex][columnIndex] = '.';
                        break;
                    }
                    tempRowIndex--;
                }
            }
        }
    }

    static void Rotator(ref List<List<char>> inputList)
    {
        List<List<char>> tempList = new List<List<char>>();

        for (var columnIndex = 0; columnIndex < inputList[0].Count; columnIndex++)
        {
            List<char> tempLine = new List<char>();
            for (var rowIndex = inputList.Count - 1; rowIndex >= 0; rowIndex--)
            {
                tempLine.Add(inputList[rowIndex][columnIndex]);
            }
            tempList.Add(tempLine);
        }

        inputList = tempList;
    }
}