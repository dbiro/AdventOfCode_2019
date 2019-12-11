using System;

namespace AdventOfCode._2019.Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] program = InputReader.Read();                                   
            
            int[] result = IntcodeProgramExecutor.Execute(program);

            Console.WriteLine(string.Join(',', result));
        }
    }
}
