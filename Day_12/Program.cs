using System.Collections.Generic;

public class Day12
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

            int solution1 = 0;

            for (var lineIndex = 0; lineIndex < riddleList.Count; lineIndex++)
            {
                string riddleInput = riddleList[lineIndex];
                List<int> instructionInput = instructionList[lineIndex];

                int lineSolution = CalculatePossibilities(riddleInput, "", instructionInput);
                solution1 += lineSolution;
            }

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
                for(var i = 0; i < 5; i++)
                {
                    riddle += splitLine[0];
                    riddle += "?";
                }

                riddle = riddle.Remove(riddle.Length -1, 1);
                riddleList.Add(riddle);

                var splitNumbers = splitLine[1].Split(',');

                List<int> lineList = new List<int>();

                foreach (var number in splitNumbers)
                {
                    lineList.Add(int.Parse(number));
                }

                List<int> instruction = new List<int>();
                for(var i = 0; i < 5; i++)
                {
                    instruction.AddRange(lineList);
                }
                instructionList.Add(instruction);
            }

            int solution1 = 0;

            for (var lineIndex = 0; lineIndex < riddleList.Count; lineIndex++)
            {
                string riddleInput = riddleList[lineIndex];
                List<int> instructionInput = instructionList[lineIndex];

                int lineSolution = CalculatePossibilities(riddleInput, "", instructionInput);
                solution1 += lineSolution;

                Console.WriteLine($"Solution for line {lineIndex + 1} is {lineSolution}");
            }

            Console.WriteLine($"Solution 1 is {solution1}");

        }
    }

    static bool IsValidCombination(string solution, List<int> instructions)
    {

        List<int> possibleSolutions = new List<int>();
        possibleSolutions.Add(0);
        int brokenCounter = 0;

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
                return false;
            }
        }

        return true;
    }

    static int CalculatePossibilities(string riddle, string partSolution, List<int> instructions)
    {
        if (partSolution.Length == riddle.Length)
        {
            bool isValid = IsValidCombination(partSolution, instructions);
            return isValid ? 1 : 0;
        }

        int startIndex = partSolution.Length;
        char riddleSymbol = riddle[startIndex];

        int validSolutions = 0;

        switch (riddleSymbol)
        {
            case '?':
                //Damaged path
                validSolutions += CalculatePossibilities(riddle, partSolution + "#", instructions);
                //Working path
                validSolutions += CalculatePossibilities(riddle, partSolution + ".", instructions);
                break;

            case '#':
                validSolutions += CalculatePossibilities(riddle, partSolution + "#", instructions);
                break;
            case '.':
                validSolutions += CalculatePossibilities(riddle, partSolution + ".", instructions);
                break;
        }

        return validSolutions;
    }
}