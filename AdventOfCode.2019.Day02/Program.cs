using System;
using System.Collections.Immutable;
using System.Linq;

namespace AdventOfCode._2019.Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            ImmutableArray<int> input = ImmutableArray.Create(InputReader.Read());

            int[] firstInput = input.ToArray();
            firstInput[1] = 12; // noun
            firstInput[2] = 2;  // verb
            int[] output = IntcodeProgramExecutor.Execute(firstInput);

            Console.WriteLine(output[0]);

            bool isInputFound = false;
            int expectedOutput = 19690720;

            for (int n = 0; !isInputFound && n < 100; n++)
            {
                if (isInputFound)
                {
                    break;
                }
                for (int v = 0; !isInputFound && v < 100; v++)
                {
                    int[] currentInput = input.ToArray();
                    currentInput[1] = n;
                    currentInput[2] = v;

                    try
                    {
                        int[] currentOutput = IntcodeProgramExecutor.Execute(currentInput);
                        if (currentOutput[0] == expectedOutput)
                        {
                            isInputFound = true;
                            Console.WriteLine($"expected output: {expectedOutput}, noun: {n}, verb: {v}, answer: {100 * n + v}");
                        }                        
                    }
                    catch
                    {
                        // no-op
                    }
                }
            }
        }
    }
}
