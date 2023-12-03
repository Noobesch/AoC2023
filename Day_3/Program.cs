public class EngineNumber
{
    public EngineNumber()
    {

    }
    public EngineNumber(EngineNumber other)
    {
        this.startIndex = other.startIndex;
        this.endIndex = other.endIndex;
        this.containedNumber = other.containedNumber;
    }
    public int startIndex;
    public int endIndex;
    public int containedNumber;
}

public class Gear
{
    public Gear()
    {

    }

    public Gear(Gear other)
    {
        position = other.position;
        firstGearComp = other.firstGearComp;
        secondGearComp = other.secondGearComp;
    }
    public int position;
    public int firstGearComp;
    public int secondGearComp;
}

public class Day3
{
    static void Main()
    {
        Part1();
        Part2();
    }

    static void Part1()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string input = reader.ReadToEnd();
            int lineLength = input.IndexOf("\r\n");

            input = input.Replace("\r\n", "");

            List<EngineNumber> numberList = new List<EngineNumber>();
            List<int> symbolsList = new List<int>();

            string currentNumString = "";
            EngineNumber engineNumber = null;


            for (var charIndex = 0; charIndex < input.Length; charIndex++)
            {
                var character = input[charIndex];

                if (int.TryParse(character.ToString(), out int intChar))
                {
                    if (engineNumber == null)
                    {
                        engineNumber = new EngineNumber();
                        engineNumber.startIndex = charIndex;
                    }

                    currentNumString += character;
                }
                else
                {
                    if (engineNumber != null)
                    {
                        engineNumber.endIndex = charIndex - 1;
                        engineNumber.containedNumber = int.Parse(currentNumString);
                        numberList.Add(new EngineNumber(engineNumber));

                        currentNumString = "";
                        engineNumber = null;
                    }

                    if (character != '.')
                    {
                        symbolsList.Add(charIndex);
                    }
                }
            }

            int[] neighbourArray = new int[]
            {
                -lineLength - 1, -lineLength, -lineLength + 1,
                -1,1,
                lineLength -1, lineLength, lineLength +1
                };

            int solution1 = 0;

            foreach (var number in numberList)
            {
                int start = number.startIndex;
                int end = number.endIndex;
                bool found = false;


                foreach (int symbol in symbolsList)
                {
                    if (found)
                    {
                        break;
                    }
                    foreach (int neighbour in neighbourArray)
                    {
                        if ((symbol + neighbour) >= start &&
                        (symbol + neighbour) <= end)
                        {
                            solution1 += number.containedNumber;
                            found = true;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine($"Solution of 1 is {solution1}");
        }
    }

    static void Part2()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            string input = reader.ReadToEnd();
            int lineLength = input.IndexOf("\r\n");

            input = input.Replace("\r\n", "");

            List<EngineNumber> numberList = new List<EngineNumber>();
            List<int> possibleGearList = new List<int>();

            string currentNumString = "";
            EngineNumber engineNumber = null;


            for (var charIndex = 0; charIndex < input.Length; charIndex++)
            {
                var character = input[charIndex];

                if (int.TryParse(character.ToString(), out int intChar))
                {
                    if (engineNumber == null)
                    {
                        engineNumber = new EngineNumber();
                        engineNumber.startIndex = charIndex;
                    }

                    currentNumString += character;
                }
                else
                {
                    if (engineNumber != null)
                    {
                        engineNumber.endIndex = charIndex - 1;
                        engineNumber.containedNumber = int.Parse(currentNumString);
                        numberList.Add(new EngineNumber(engineNumber));

                        currentNumString = "";
                        engineNumber = null;
                    }

                    if (character == '*')
                    {
                        possibleGearList.Add(charIndex);
                    }
                }
            }

            // foreach(var number in numberList)
            // {
            //     Console.WriteLine($"Found {number.containedNumber} from {number.startIndex} till {number.endIndex}");
            // }

            foreach (var symbol in possibleGearList)
            {
                Console.WriteLine($"Found a gear at {symbol}");
            }

            int[] neighbourArray = new int[]
            {
                -lineLength - 1, -lineLength, -lineLength + 1,
                -1,1,
                lineLength -1, lineLength, lineLength +1
                };

            int solution2 = 0;

            // foreach (var number in numberList)
            // {
            //     int start = number.startIndex;
            //     int end = number.endIndex;
            //     bool found = false;


            //     foreach (int symbol in geareList)
            //     {
            //         if (found)
            //         {
            //             break;
            //         }
            //         foreach (int neighbour in neighbourArray)
            //         {
            //             if ((symbol + neighbour) >= start &&
            //             (symbol + neighbour) <= end)
            //             {
            //                 solution2 += number.containedNumber;
            //                 found = true;
            //                 break;
            //             }
            //         }
            //     }
            // }

            // Console.WriteLine($"Solution of 2 is {solution2}");
        }
    }
}