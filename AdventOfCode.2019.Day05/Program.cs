using System;

namespace AdventOfCode._2019.Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] program = InputReader.Read();                                   
            // int[] program = new int[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 };

            // Console.WriteLine(string.Join(',', program));
            // IntcodeProgramExecutor.EnableVerboseMode = true;

            IntcodeProgramExecutor.Execute(program);
        }
    }
}
