using System;

namespace AdventOfCode._2019.Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            //long[] input = InputReader.Read();
            long[] input = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };

            var program = new IntcodeProgram(input);
            program.Execute();
        }
    }
}
