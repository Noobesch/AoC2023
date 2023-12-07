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

            Console.WriteLine("a");

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
                    i+= count;
                }

                appearanceList.Sort();


                if (hand[0] == hand[1] &&
                hand[0] == hand[2] &&
                hand[0] == hand[3] &&
                hand[0] == hand[4])
                {
                    fiveHand = InsertIntoList(fiveHand, tuple);
                }
                else if (hand[0] == hand[1] &&
                hand[0] == hand[2] &&
                hand[0] == hand[3])
                {
                    fourHand = InsertIntoList(fourHand, tuple);
                }
                else if (hand[0] == hand[1] &&
                hand[0] == hand[2] &&
                hand[3] == hand[4])
                {
                    fHHand = InsertIntoList(fHHand, tuple);
                }
                else if (hand[0] == hand[1] &&
                hand[0] == hand[2])
                {
                    threeHand = InsertIntoList(threeHand, tuple);
                }
                else if (hand[0] == hand[1] &&
                hand[2] == hand[3])
                {
                    tPHand = InsertIntoList(tPHand, tuple);
                }
                else if (hand[0] == hand[1])
                {
                    oPHand = InsertIntoList(oPHand, tuple);
                }
                else
                {
                    hcHand = InsertIntoList(hcHand, tuple);
                }
            }

            sortedList.AddRange(fiveHand);

            Console.WriteLine(";");
        }
    }

    static List<(string hand, int bid)> InsertIntoList(List<(string hand, int bid)> ListToInsert, (string hand, int bid) tuple)
    {
        if (ListToInsert.Count == 0)
        {
            ListToInsert.Add(tuple);
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

                    if (listRanking == newRanking)
                    {
                        continue;
                    }
                    else if (newRanking < listRanking)
                    {
                        ListToInsert.Insert(tupleIndex, tuple);
                        return (ListToInsert);
                    }
                }
            }
        }
        throw new Exception();
    }
}