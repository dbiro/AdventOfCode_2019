using System;

namespace AdventOfCode._2019.Day09
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = InputReader.Read();
            
            //long[] input = new long[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 };
            //long[] input = new long[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 };
            //long[] input = new long[] { 104, 1125899906842624, 99 };
                        
            //long[] input = new long[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

            var program = new IntcodeProgram(input);
            program.Execute();
            Console.WriteLine($"halted: {program.Halted}, waitingForInput: {program.WaitingForInput}");
        }
    }
}
