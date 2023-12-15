using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

public class Day15
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
        using (StreamReader reader = new StreamReader(path))
        {
            List<string> inputList = reader.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            long solution1 = 0;
            foreach (var input in inputList)
            {
                long partSolution = 0;

                foreach (var character in input)
                {
                    partSolution += (int)character;
                    partSolution *= 17;
                    partSolution = partSolution % 256;
                }

                solution1 += partSolution;
            }

            Console.WriteLine($"Part solution1 is {solution1}");
        }
    }

    static void Part2(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            List<string> inputList = reader.ReadLine().Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            List<(string label, int focalLength)>[] boxList = new List<(string label, int focalLength)>[256];

            for (var i = 0; i < boxList.Length; i++)
            {
                boxList[i] = new List<(string label, int focalLength)>();
            }

            foreach (var input in inputList)
            {
                long box = 0;
                char sign = '+';
                int focalLength = -1;
                int charCounter = 0;

                bool signFound = false;
                foreach (var character in input)
                {
                    if (character == '-' || character == '=')
                    {
                        sign = character;
                        signFound = true;
                        continue;
                    }

                    if (signFound)
                    {
                        focalLength = int.Parse(character.ToString());
                        break;
                    }


                    box += (int)character;
                    box *= 17;
                    box = box % 256;
                    charCounter++;
                }


                string cleanedInput = input.Remove(charCounter);
                var containedTuple = boxList[box].Find(x => x.label == cleanedInput);

                if (sign == '-')
                {
                    if (containedTuple != default((string, int)))
                    {
                        boxList[box].Remove(containedTuple);
                    }
                }
                else
                {
                    if (!boxList[box].Contains(containedTuple))
                    {
                        boxList[box].Add((cleanedInput, focalLength));
                    }
                    else
                    {
                        int index = boxList[box].IndexOf(containedTuple);
                        boxList[box][index] = (cleanedInput, focalLength);
                    }
                }
            }

            long solution2 = 0;

            for(var boxIndex = 0; boxIndex < boxList.Length; boxIndex++)
            {
                var box = boxList[boxIndex];

                for(var lenseIndex = 0; lenseIndex < box.Count; lenseIndex++)
                {
                    var tempAdd =(boxIndex + 1) *  (lenseIndex + 1) * box[lenseIndex].focalLength;
                    solution2 += tempAdd;
                }
            }
            Console.WriteLine($"Part solution 2 is {solution2}");
        }
    }
}