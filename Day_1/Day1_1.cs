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
            Dictionary<string, int> ConversionDict = new Dictionary<string, int>()
            {
                ["one"] = 1,
                ["two"] = 2,
                ["three"] = 3,
                ["four"] = 4,
                ["five"] = 5,
                ["six"] = 6,
                ["seven"] = 7,
                ["eight"] = 8,
                ["nine"] = 9
            };

            string[] lines = reader.ReadToEnd().Split("\r\n");
            int sum = 0;

            for (var lineIndex = 0; lineIndex < lines.Length; lineIndex++)
            {
                string line = lines[lineIndex];
                string tempLine = "";
                string lineSolution = "";
                List<string> lineSolutions = new List<string>();

                for (var characterIndex = 0; characterIndex < line.Length; characterIndex++)
                {
                    string character = line[characterIndex].ToString();
                    tempLine += character;


                    if (int.TryParse(character, out int worked))
                    {
                        lineSolutions.Add(character);
                        tempLine = "";
                    }

                    foreach (var key in ConversionDict.Keys)
                    {
                        if (tempLine.Contains(key))
                        {
                            lineSolutions.Add((ConversionDict[key]).ToString());
                            tempLine = "";
                            break;
                        }
                    }
                }

                lineSolution = lineSolutions[0] + lineSolutions[lineSolutions.Count - 1];
                sum += int.Parse(lineSolution);
            }
            Console.WriteLine(sum); //54978 higher
        }
    }
}