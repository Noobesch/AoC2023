using System.Diagnostics;
using System.Security.AccessControl;

public class Day7
{
    static char[] strengthArray = new char[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

    static void Main()
    {
        string path = "Input_1.txt";
        path = "../../../Input_1.txt";
        Part1(path);
    }

    static void Part1(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {

            List<string> hands = new List<string>();
            List<int> bids = new List<int>();

            List<(string hand, int bid)> tupleList = new List<(string hand, int bid)>();

            while (!reader.EndOfStream)
            {
                var splitLine = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                hands.Add(splitLine[0]);
                bids.Add(int.Parse(splitLine[1]));

                tupleList.Add((splitLine[0], int.Parse(splitLine[1])));
            }

            tupleList.Reverse();

            List<(string hand, int bid)> sortedList = new List<(string hand, int bid)>();

            List<(string hand, int bid)> fiveHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> fourHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> fHHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> threeHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> tPHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> oPHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> hcHand = new List<(string hand, int bid)>();



            for (var entryIndex = 0; entryIndex < tupleList.Count; entryIndex++)
            {
                var tuple = tupleList[entryIndex];

                var hand = String.Concat(tuple.hand.OrderBy(c => c));

                int i = 0;
                List<int> appearanceList = new List<int>();
                while (i < hand.Length)
                {
                    int count = hand.Split(hand[i]).Length - 1;

                    appearanceList.Add(count);
                    i += count;
                }

                appearanceList.Sort();
                appearanceList.Reverse();


                if (appearanceList[0] == 5)
                {
                    fiveHand = InsertIntoList(fiveHand, tuple);
                }
                else if (appearanceList[0] == 4)
                {
                    fourHand = InsertIntoList(fourHand, tuple);
                }
                else if (appearanceList[0] == 3 &&
                appearanceList[1] == 2)
                {
                    fHHand = InsertIntoList(fHHand, tuple);
                }
                else if (appearanceList[0] == 3)
                {
                    threeHand = InsertIntoList(threeHand, tuple);
                }
                else if (appearanceList[0] == 2 &&
                appearanceList[1] == 2)
                {
                    tPHand = InsertIntoList(tPHand, tuple);
                }
                else if (appearanceList[0] == 2)
                {
                    oPHand = InsertIntoList(oPHand, tuple);
                }
                else
                {
                    hcHand = InsertIntoList(hcHand, tuple);
                }
            }

            sortedList.AddRange(hcHand);
            sortedList.AddRange(oPHand);
            sortedList.AddRange(tPHand);
            sortedList.AddRange(threeHand);
            sortedList.AddRange(fHHand);
            sortedList.AddRange(fourHand);
            sortedList.AddRange(fiveHand);

            int solution1 = 0;

            for (var i = 0; i < sortedList.Count; i++)
            {
                solution1 += (i + 1) * sortedList[i].bid;
            }

            Console.WriteLine($"Solution 1 is {solution1}");
        }
    }

    static List<(string hand, int bid)> InsertIntoList(List<(string hand, int bid)> ListToInsert, (string hand, int bid) tuple)
    {
        int smallestInsert = 0;

        if (ListToInsert.Count == 0)
        {
            ListToInsert.Add(tuple);
            return ListToInsert;
        }
        else
        {
            for (var tupleIndex = 0; tupleIndex < ListToInsert.Count; tupleIndex++)
            {
                var entryHand = ListToInsert[tupleIndex].hand;

                for (var handIndex = 0; handIndex < entryHand.Length; handIndex++)
                {
                    var listRanking = Array.IndexOf(strengthArray, entryHand[handIndex]);
                    var newRanking = Array.IndexOf(strengthArray, tuple.hand[handIndex]);

                    if (listRanking < newRanking)
                    {
                        smallestInsert++;
                        break;

                    }
                    else if (listRanking > newRanking)
                    {
                        break;
                    }
                }
            }
        }
        ListToInsert.Insert(smallestInsert, tuple);
        return ListToInsert;
    }
}