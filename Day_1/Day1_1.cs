using System;
using System.Collections.Generic;

class Day1_1
{
    static void Main(string[] args)
    {
        Task1();
        Task2();
    }

    static void Task1()
    {
        using (StreamReader reader = new StreamReader("input_1.txt"))
        {
            List<string> inputList = new List<string>();
            while (!reader.EndOfStream)
            {
                inputList.Add(reader.ReadLine());
            }

            List<int> solutionArray = new List<int>();
            var solutionSum = 0;
            foreach (var line in inputList)
            {
                char firstChar = '-';
                char lastChar = '-';
                string lineSolution = "";
                foreach (var character in line)
                {
                    if ((int)character >= 48 &&
                    (int)character <= 57)
                    {
                        if (firstChar == '-')
                        {
                            firstChar = character;
                        }
                        else
                        {
                            lastChar = character;
                        }

                    }
                }

                if (lastChar == '-')
                {
                    lastChar = firstChar;
                }

                lineSolution = firstChar.ToString() + lastChar.ToString();

                solutionArray.Add(int.Parse(lineSolution));
                solutionSum += int.Parse(lineSolution);
            }

            Console.WriteLine(solutionSum);
        }
    }

    static void Task2()
    {
        using (StreamReader reader = new StreamReader("input_2.txt"))
        {
            List<string> inputList = new List<string>();
            while (!reader.EndOfStream)
            {
                inputList.Add(reader.ReadLine());
            }

            Dictionary<string, int> ConversionDict = new Dictionary<string, int>()
            {
                ["one"] = 49,
                ["two"] = 50,
                ["three"] = 51,
                ["four"] = 52,
                ["five"] = 53,
                ["six"] = 54,
                ["seven"] = 55,
                ["eight"] = 56,
                ["nine"] = 57
            };


            long solutionSum = 0;
            foreach (var line in inputList)
            {
                char firstChar = '-';
                char lastChar = '-';

                string lineSolution = "";
                string tempLineBuffer = "";

                foreach (var character in line)
                {
                    tempLineBuffer += character;
                    var tempCharacter = character;

                    foreach (var key in ConversionDict.Keys)
                    {
                        if (tempLineBuffer.Contains(key))
                        {
                            tempCharacter = (char)ConversionDict[key];
                        }
                    }

                    if ((int)tempCharacter >= 49 &&
                    (int)tempCharacter <= 57)
                    {
                        tempLineBuffer = "";

                        if (firstChar == '-')
                        {
                            firstChar = tempCharacter;
                        }
                        else
                        {
                            lastChar = tempCharacter;
                        }
                    }
                }

                if (lastChar == '-')
                {
                    lastChar = firstChar;
                }

                lineSolution = firstChar.ToString() + lastChar.ToString();
                solutionSum += int.Parse(lineSolution);

                Console.WriteLine(lineSolution);
            }

            Console.WriteLine(solutionSum);
        }
    }
}