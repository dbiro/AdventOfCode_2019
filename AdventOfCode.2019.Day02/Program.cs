using System;

namespace AdventOfCode._2019.Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = InputReader.Read();
            input[1] = 12;
            input[2] = 2;

            int[] output = IntcodeProgramExecutor.Execute(input);

            Console.WriteLine(output[0]);
        }
    }
}
