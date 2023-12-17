using System.Collections.Generic;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class Node
{
    public Node(int Value)
    {
        this.Value = Value;
    }

    public Node()
    {

    }


    public int Value;
    public List<int> MinimalArrivalCostList = new List<int>();
    public List<(Direction direction, Node node)> Neighbours = new List<(Direction direction, Node node)>();

    private List<(Direction direction, int row, int column)> _neighbourLocations = new List<(Direction direction, int row, int column)>()
    {
        (Direction.North, -1, 0),
        (Direction.East, 0, 1),
        (Direction.South, 1, 0),
        (Direction.West, 0, -1)
    };

    public Node Predecessor = null;

    public override string ToString()
    {
        return $"Value {Value}";
    }

    public void SetNeighbours(List<List<Node>> nodeList, int row, int column)
    {
        foreach (var tuple in _neighbourLocations)
        {
            int rowIndex = row + tuple.row;
            int columnIndex = column + tuple.column;

            if (rowIndex < 0 || columnIndex < 0 ||
            rowIndex >= nodeList.Count || columnIndex >= nodeList[0].Count)
            {
                continue;
            }

            Node neighbour = nodeList[rowIndex][columnIndex];
            if (neighbour != this)
            {
                Neighbours.Add((tuple.direction, neighbour));
            }
        }

    }
}
public class Day17
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
        List<List<Node>> nodeList = new List<List<Node>>();
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\r\n", "");
                List<Node> lineList = new List<Node>();
                foreach (var character in line)
                {
                    Node node = new Node(int.Parse(character.ToString()));
                    lineList.Add(node);
                }

                nodeList.Add(lineList);
            }
        }


        for (var rowIndex = 0; rowIndex < nodeList.Count; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < nodeList[0].Count; columnIndex++)
            {
                Node node = nodeList[rowIndex][columnIndex];
                node.SetNeighbours(nodeList, rowIndex, columnIndex);
            }
        }

        FindPath(nodeList[0][0], 0, Direction.East);
        FindPath(nodeList[0][0], 0, Direction.South);
        int smallestVal = nodeList[nodeList.Count - 1][nodeList[0].Count - 1].MinimalArrivalCostList.Min();
        Console.WriteLine($"The final part has the minimum cost of {smallestVal}");


        // List<char[]> inputArray = new List<char[]>();
        // List<char[]> solutionArray = new List<char[]>();

        // using (StreamReader reader = new StreamReader(path))
        // {
        //     while (!reader.EndOfStream)
        //     {
        //         inputArray.Add(reader.ReadLine().Replace("\r\n", "").ToCharArray());
        //     }
        // }
        // using (StreamReader reader = new StreamReader("../../../Solution_1.txt"))
        // {
        //     while (!reader.EndOfStream)
        //     {
        //         solutionArray.Add(reader.ReadLine().Replace("\r\n", "").ToCharArray());
        //     }
        // }

        // int counter = 0;
        // for (var rowIndex = 0; rowIndex < solutionArray.Count; rowIndex++)
        // {
        //     for (var columnIndex = 0; columnIndex < solutionArray[0].Length; columnIndex++)
        //     {
        //         if (solutionArray[rowIndex][columnIndex] == inputArray[rowIndex][columnIndex])
        //         {
        //             Console.Write(" ");
        //         }
        //         else
        //         {
        //             Console.Write(inputArray[rowIndex][columnIndex]);
        //             counter += int.Parse(inputArray[rowIndex][columnIndex].ToString());
        //         }
        //     }
        //     Console.WriteLine();
        // }
        // Console.WriteLine($"That are {counter} steps");
    }
    static void FindPath(Node currentNode, int straightLineCounter, Direction lastDirection)
    {
        foreach (var neighbourTuple in currentNode.Neighbours)
        {
            int localStraightCounter = straightLineCounter;
            Direction localLastDirection = lastDirection;
            if (neighbourTuple.direction == lastDirection)
            {
                localStraightCounter++;
            }
            else
            {
                localLastDirection = neighbourTuple.direction;
                localStraightCounter = 0;
            }
            if (localStraightCounter >= 3)
            {
                continue;
            }

            Node neighbour = neighbourTuple.node;
            int localCost = currentNode.MinimalArrivalCost + neighbour.Value;

            if (localCost >= neighbour.MinimalArrivalCost && neighbour.MinimalArrivalCost != 0)
            {
                continue;
            }
            neighbour.MinimalArrivalCost = localCost;
            neighbour.Predecessor = currentNode;

            FindPath(neighbour, localStraightCounter, localLastDirection);
        }
    }
}