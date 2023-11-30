using System;


class Day1_1
{
    static void Main(string[] args)
    {
        using (StreamReader reader = new StreamReader("input_1.txt"))
        {
            while (!reader.EndOfStream)
            {
                Console.WriteLine(reader.ReadLine());
            }
        }
    }
}