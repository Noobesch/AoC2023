using System.Collections.Generic;

public class Day12
{
    static List<List<string>> pseudoValidLines = new List<List<string>>();
    static void Main()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
        Part1(path);
        // Part2(path);
    }

    static void Part1(string path)
    {
        List<string> riddleList = new List<string>();
        List<List<int>> instructionList = new List<List<int>>();

        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var splitLine = reader.ReadLine().Split(" ");

                riddleList.Add(splitLine[0]);

                var splitNumbers = splitLine[1].Split(',');

                List<int> lineList = new List<int>();

                foreach (var number in splitNumbers)
                {
                    lineList.Add(int.Parse(number));
                }

                instructionList.Add(lineList);
            }

            long solution1 = 0;

            for (var lineIndex = 0; lineIndex < riddleList.Count; lineIndex++)
            {
                string riddleInput = riddleList[lineIndex];
                List<int> instructionInput = instructionList[lineIndex];

                pseudoValidLines.Add(new List<string>());

                long lineSolution = CalculatePossibilities(riddleInput, "", instructionInput, lineIndex);
                solution1 += lineSolution;
            }

            int pseudoLinesCount = pseudoValidLines.Sum(x => x.Count);

            Console.WriteLine($"Solution 1 is {solution1}");

        }
    }

    static void Part2(string path)
    {
        List<string> riddleList = new List<string>();
        List<List<int>> instructionList = new List<List<int>>();

        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                var splitLine = reader.ReadLine().Split(" ");

                string riddle = "";
                for (var i = 0; i < 5; i++)
                {
                    riddle += splitLine[0];
                    riddle += "?";
                }

                riddle = riddle.Remove(riddle.Length - 1, 1);
                riddleList.Add(riddle);

                var splitNumbers = splitLine[1].Split(',');

                List<int> lineList = new List<int>();

                foreach (var number in splitNumbers)
                {
                    lineList.Add(int.Parse(number));
                }

                List<int> instruction = new List<int>();
                for (var i = 0; i < 5; i++)
                {
                    instruction.AddRange(lineList);
                }
                instructionList.Add(instruction);
            }

            long solution2 = 0;

            for (var lineIndex = 0; lineIndex < riddleList.Count; lineIndex++)
            {
                string riddleInput = riddleList[lineIndex];
                List<int> instructionInput = instructionList[lineIndex];

                long lineSolution = CalculatePossibilities(riddleInput, "", instructionInput, lineIndex);
                solution2 += lineSolution;

                Console.WriteLine($"Solution for line {lineIndex + 1} is {lineSolution}");
            }

            Console.WriteLine($"Solution 2 is {solution2}");

        }
    }

    static bool IsValidCombination(string solution, List<int> instructions, int lineIndex)
    {
        List<int> possibleSolutions = new List<int>();
        possibleSolutions.Add(0);

        for (var i = 0; i < solution.Length; i++)
        {
            char character = solution[i];

            if (character == '#')
            {
                possibleSolutions[possibleSolutions.Count - 1]++;
            }
            else
            {
                possibleSolutions.Add(0);
            }
        }

        possibleSolutions.RemoveAll(x => x == 0);

        if (instructions.Count != possibleSolutions.Count)
        {
            return false;
        }

        for (var i = 0; i < instructions.Count; i++)
        {
            int instruction = instructions[i];
            int possibleSolution = possibleSolutions[i];

            if (instruction != possibleSolution)
            {
                if (possibleSolution < instruction)
                {
                    pseudoValidLines[lineIndex].Add(solution);
                }
                return false;
            }
        }
        pseudoValidLines[lineIndex].Add(solution);
        return true;
    }

    static bool FastCheck(string partSolution, List<int> instructions)
    {
        List<int> possibleSolutions = new List<int>();
        possibleSolutions.Add(0);

        for (var i = 0; i < partSolution.Length; i++)
        {
            char character = partSolution[i];

            if (character == '#')
            {
                possibleSolutions[possibleSolutions.Count - 1]++;
            }
            else
            {
                possibleSolutions.Add(0);
            }
        }

        possibleSolutions.RemoveAll(x => x == 0);

        if (instructions.Count < possibleSolutions.Count)
        {
            return false;
        }

        for (var i = 0; i < possibleSolutions.Count; i++)
        {
            int instruction = instructions[i];
            int possibleSolution = possibleSolutions[i];

            if (instruction != possibleSolution)
            {
                return false;
            }
        }

        return true;
    }

    static long CalculatePossibilities(string riddle, string partSolution, List<int> instructions, int lineIndex)
    {
        if (partSolution.Length > 0)
        {
            char lastChar = partSolution[partSolution.Length - 1];

            if (lastChar == '.')
            {
                if (!FastCheck(partSolution, instructions))
                {
                    return 0;
                }
            }

        }

        if (partSolution.Length == riddle.Length)
        {
            bool isValid = IsValidCombination(partSolution, instructions, lineIndex);
            return isValid ? 1 : 0;
        }

        int startIndex = partSolution.Length;
        char riddleSymbol = riddle[startIndex];

        long validSolutions = 0;

        switch (riddleSymbol)
        {
            case '?':
                //Damaged path
                validSolutions += CalculatePossibilities(riddle, partSolution + "#", instructions, lineIndex);
                //Working path
                validSolutions += CalculatePossibilities(riddle, partSolution + ".", instructions, lineIndex);
                break;

            case '#':
                validSolutions += CalculatePossibilities(riddle, partSolution + "#", instructions, lineIndex);
                break;
            case '.':
                validSolutions += CalculatePossibilities(riddle, partSolution + ".", instructions, lineIndex);
                break;
        }

        return validSolutions;
    }
}