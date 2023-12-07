using System.Diagnostics;
using System.Security.AccessControl;

public class Day7
{
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
            char[] strengthArray = new char[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

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
            List<(string hand, int bid)> ThreeHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> tPHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> oPHand = new List<(string hand, int bid)>();
            List<(string hand, int bid)> hcHand = new List<(string hand, int bid)>();



            for (var entryIndex = 0; entryIndex < tupleList.Count; entryIndex++)
            {
                var tuple = tupleList[entryIndex];
                
                if(tupleList.Count == 0)
                {
                    tupleList.Add(tuple);
                    continue;
                }

               var hand =  String.Concat(tuple.hand.OrderBy(c => c));

                if( hand[0] == hand[1] && hand[0] == hand[2] &&
                hand[0] == hand[3] &&
                hand[0] == hand[4])
                {
                    if(fiveHand.Count == 0)
                    {
                        fiveHand.Add(tuple);
                    }
                    else
                    {
                        bool handFound = false;
                        for(var tupleIndex = 0; tupleIndex < fiveHand.Count; tupleIndex++)
                        {
                            var entryHand = fiveHand[tupleIndex].hand;

                            for(var handIndex = 0; handIndex < entryHand.Length; handIndex++)
                            {
                                var listRanking = Array.IndexOf(strengthArray, entryHand[handIndex]);
                                var newRanking = Array.IndexOf(strengthArray, tuple.hand[handIndex]);

                                if(listRanking == newRanking)
                                {
                                    continue;
                                }
                                else if(newRanking < listRanking)
                                {
                                    fiveHand.Insert(tupleIndex, tuple);
                                    handFound = true;
                                    break;
                                }
                            }
                            if(handFound)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(";");
        }
    }
}