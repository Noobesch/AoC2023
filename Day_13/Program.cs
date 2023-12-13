using System.Collections.Generic;

public class Day13
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
        List<List<string>> rowListList = new List<List<string>>();
        List<List<string>> columnListList = new List<List<string>>();


        using (StreamReader reader = new StreamReader(path))
        {
            List<string> rowList = new List<string>();
            List<string> columnList = new List<string>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\r\n", "");

                if (line == "" || reader.EndOfStream)
                {
                    rowListList.Add(rowList);

                    for (var columnIndex = 0; columnIndex < rowList[0].Length; columnIndex++)
                    {
                        string column = "";
                        for (var rowIndex = 0; rowIndex < rowList.Count; rowIndex++)
                        {
                            column += rowList[rowIndex][columnIndex];
                        }
                        columnList.Add(column);
                    }

                    columnListList.Add(columnList);

                    rowList = new List<string>();
                    columnList = new List<string>();
                }
                else
                {
                    rowList.Add(line);
                }
            }
        }

        long solution1 = 0;

        solution1 += CalculateMirrors(rowListList, true);
        solution1 += CalculateMirrors(columnListList, false);


        Console.WriteLine($"Solution 1 ist {solution1}");
    }

    static long CalculateMirrors(List<List<string>> ListList, bool isRow)
    {
        long partSolution = 0;
        int listCounter = 0;
        foreach (var List in ListList)
        {
            listCounter++;
            int mirrorIndexLeft = int.MinValue;
            int mirrorIndexRight = int.MinValue;

            HashSet<string> mirrorSet = new HashSet<string>();
            long tempSolution = 0;

            for (var index = 0; index < List.Count; index++)
            {
                string line = List[index];

                if (!mirrorSet.Contains(line))
                {
                    mirrorSet.Add(line);
                    continue;
                }

                mirrorIndexLeft = index - 1;
                mirrorIndexRight = index;

                while (true)
                {
                    if (mirrorIndexLeft < 0 || mirrorIndexRight >= List.Count)
                    {
                        tempSolution = isRow ? 100 * index : index;
                        break;
                    }
                    if (List[mirrorIndexLeft] == List[mirrorIndexRight])
                    {
                        mirrorIndexLeft--;
                        mirrorIndexRight++;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            partSolution += tempSolution;
        }

        return partSolution;
    }
}