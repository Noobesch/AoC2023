﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

public enum Direction
{
    North,
    East,
    South,
    West,
    None
}

public class Beam
{
    public Beam()
    {

    }

    public Beam((int row, int column) pos, List<(int row, int column)> previousPath)
    {
        currentPos = pos;
        path = new List<(int row, int column)>(previousPath);
    }
    public List<(int row, int column)> path = new List<(int row, int column)>();
    //Set this to your first puzzle direction
    public Direction Direction = Direction.None;
    private (int row, int column) currentPos;

    public void SetPosition((int row, int column) newPos)
    {
        currentPos = newPos;
        path.Add(currentPos);
    }

    public (int row, int column) GetPosition()
    {
        return currentPos;
    }
}

public class Day16
{
    static List<List<char>> inputList = new List<List<char>>();
    // static bool[,] litArray;
    // static List<Direction>[,] directionList;
    static void Main()
    {
        string path = "Input_1.txt";
        // path = "../../../Input_1.txt";
        Part1(path);
        Part2(path);
    }

    static void Part1(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\r\n", "");
                List<char> lineList = line.ToCharArray().ToList();
                inputList.Add(lineList);
            }
        }

        List<Direction>[,] directionList; directionList = new List<Direction>[inputList.Count, inputList[0].Count];

        for (var rowIndex = 0; rowIndex < directionList.GetLength(0); rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < directionList.GetLength(1); columnIndex++)
            {
                directionList[rowIndex, columnIndex] = new List<Direction>();
            }
        }

        bool[,] litArray = new bool[inputList.Count, inputList[0].Count];
        Beam beam = new Beam();

        char startSymbol = inputList[0][0];

        switch (startSymbol)
        {
            case '.': beam.Direction = Direction.East; break;
            case '\\': beam.Direction = Direction.South; break;
            case '/': beam.Direction = Direction.North; break;
            case '-': beam.Direction = Direction.East; break;
            case '|': beam.Direction = Direction.South; break;
        }

        PathFinder(beam, directionList, litArray);

        long solution1 = 0;

        for (var rowIndex = 0; rowIndex < litArray.GetLength(0); rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < litArray.GetLength(1); columnIndex++)
            {
                bool isLit = litArray[rowIndex, columnIndex];
                if (isLit)
                {
                    Console.Write('#');
                    solution1++;
                }
                else
                {
                    Console.Write('.');
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Done, solution 1 is {solution1}");
    }

    static void Part2(string path)
    {
        long solution2 = 0;
        int cycleCount = 0;


        for (var rowIndex = 0; rowIndex < 1; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < inputList.Count; columnIndex++)
            {
                char symbol = inputList[rowIndex][columnIndex];
                cycleCount++;
                Startup(rowIndex, columnIndex, ref solution2, Direction.South);
            }
        }
        for (var rowIndex = inputList.Count - 1; rowIndex > inputList.Count - 2; rowIndex--)
        {
            for (var columnIndex = 0; columnIndex < inputList.Count; columnIndex++)
            {
                cycleCount++;
                Startup(rowIndex, columnIndex, ref solution2, Direction.North);


            }
        }


        for (var columnIndex = 0; columnIndex < 1; columnIndex++)
        {
            for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
            {
                cycleCount++;
                Startup(rowIndex, columnIndex, ref solution2, Direction.East);


            }
        }
        for (var columnIndex = inputList.Count - 1; columnIndex > inputList.Count - 2; columnIndex--)
        {
            for (var rowIndex = 0; rowIndex < inputList.Count; rowIndex++)
            {
                cycleCount++;
                Startup(rowIndex, columnIndex, ref solution2, Direction.West);


            }
        }
        Console.WriteLine($"Done, 2 solution is {solution2} after {cycleCount} cycles");

    }

    static void Startup(int rowIndex, int columnIndex, ref long solution2, Direction direction)
    {
        bool[,] litArray = new bool[inputList.Count, inputList[0].Count];

        List<Direction>[,] directionList = new List<Direction>[inputList.Count, inputList[0].Count];

        for (var directionRow = 0; directionRow < directionList.GetLength(0); directionRow++)
        {
            for (var directionColumn = 0; directionColumn < directionList.GetLength(1); directionColumn++)
            {
                directionList[directionRow, directionColumn] = new List<Direction>();
            }
        }

        Beam beam = new Beam((rowIndex, columnIndex), new List<(int row, int column)>());
        beam.Direction = direction;

        PathFinder(beam, directionList, litArray);


        long tempSol2 = 0;
        for (var solRowIndex = 0; solRowIndex < litArray.GetLength(0); solRowIndex++)
        {
            for (var solColumnIndex = 0; solColumnIndex < litArray.GetLength(1); solColumnIndex++)
            {
                bool isLit = litArray[solRowIndex, solColumnIndex];
                if (isLit)
                {
                    tempSol2++;
                }
            }
        }
        if (tempSol2 > solution2)
        {
            solution2 = tempSol2;
        }
    }

    static void PathFinder(Beam beam, List<Direction>[,] directionList, bool[,] litArray)
    {
        var currentPos = beam.GetPosition();
        var directions = directionList[currentPos.row, currentPos.column];
        litArray[currentPos.row, currentPos.column] = true;

        if (directions.Contains(beam.Direction))
        {
            return;
        }
        else
        {
            directionList[currentPos.row, currentPos.column].Add(beam.Direction);
        }

        bool didEnd = !FindNextStep(beam, out (int row, int column) newPos);

        if (didEnd)
        {
            return;
        }


        beam.SetPosition(newPos);

        char symbol = inputList[newPos.row][newPos.column];

        switch (symbol)
        {
            case '.': break;
            case '\\':
                switch (beam.Direction)
                {
                    case Direction.North: beam.Direction = Direction.West; break;
                    case Direction.East: beam.Direction = Direction.South; break;
                    case Direction.South: beam.Direction = Direction.East; break;
                    case Direction.West: beam.Direction = Direction.North; break;
                }
                break;
            case '/':
                switch (beam.Direction)
                {
                    case Direction.North: beam.Direction = Direction.East; break;
                    case Direction.East: beam.Direction = Direction.North; break;
                    case Direction.South: beam.Direction = Direction.West; break;
                    case Direction.West: beam.Direction = Direction.South; break;
                }
                break;
            case '-':
                switch (beam.Direction)
                {
                    case Direction.East: break;
                    case Direction.West: break;

                    case Direction.North:
                    case Direction.South:
                        Beam newBeam = new Beam(beam.GetPosition(), beam.path);
                        newBeam.Direction = Direction.West;
                        beam.Direction = Direction.East;
                        // List<Direction>[,] newDirectionList = new List<Direction>[inputList.Count, inputList[0].Count];

                        // for (var directionRow = 0; directionRow < directionList.GetLength(0); directionRow++)
                        // {
                        //     for (var directionColumn = 0; directionColumn < directionList.GetLength(1); directionColumn++)
                        //     {
                        //         directionList[directionRow, directionColumn] = directionList[directionRow, directionColumn];
                        //     }
                        // }

                        // bool[,] newLitArray = new bool[inputList.Count, inputList[0].Count];

                        // for (var litRow = 0; litRow < newLitArray.GetLength(0); litRow++)
                        // {
                        //     for (var litColumn = 0; litColumn < newLitArray.GetLength(1); litColumn++)
                        //     {
                        //         newLitArray[litRow, litColumn] = litArray[litRow, litColumn];
                        //     }
                        // }

                        PathFinder(newBeam, directionList, litArray);
                        break;
                }
                break;

            case '|':
                switch (beam.Direction)
                {
                    case Direction.North: break;
                    case Direction.South: break;

                    case Direction.East:
                    case Direction.West:
                        Beam newBeam = new Beam(beam.GetPosition(), beam.path);
                        newBeam.Direction = Direction.South;

                        beam.Direction = Direction.North;

                        // List<Direction>[,] newDirectionList = new List<Direction>[inputList.Count, inputList[0].Count];

                        // for (var directionRow = 0; directionRow < directionList.GetLength(0); directionRow++)
                        // {
                        //     for (var directionColumn = 0; directionColumn < directionList.GetLength(1); directionColumn++)
                        //     {
                        //         directionList[directionRow, directionColumn] = directionList[directionRow, directionColumn];
                        //     }
                        // }

                        // bool[,] newLitArray = new bool[inputList.Count, inputList[0].Count];

                        // for (var litRow = 0; litRow < newLitArray.GetLength(0); litRow++)
                        // {
                        //     for (var litColumn = 0; litColumn < newLitArray.GetLength(1); litColumn++)
                        //     {
                        //         newLitArray[litRow, litColumn] = litArray[litRow, litColumn];
                        //     }
                        // }

                        PathFinder(newBeam, directionList, litArray);
                        break;
                }
                break;
        }
        PathFinder(beam, directionList, litArray);
    }

    static bool FindNextStep(Beam beam, out (int row, int column) newPos)
    {
        newPos = beam.GetPosition();
        var direction = beam.Direction;

        switch (direction)
        {
            case Direction.North: newPos = (newPos.row - 1, newPos.column); break;
            case Direction.East: newPos = (newPos.row, newPos.column + 1); break;
            case Direction.South: newPos = (newPos.row + 1, newPos.column); break;
            case Direction.West: newPos = (newPos.row, newPos.column - 1); break;
        }

        if (newPos.row < 0 || newPos.row >= inputList.Count ||
        newPos.column < 0 || newPos.column >= inputList[0].Count)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}