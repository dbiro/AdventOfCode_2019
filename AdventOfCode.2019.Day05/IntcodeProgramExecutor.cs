using System;
using System.Linq;

namespace AdventOfCode._2019.Day05
{
    public static class IntcodeProgramExecutor
    {
        public static int[] Execute(int[] input)
        {
            return Execute(input, 0);
        }

        private static int[] Execute(int[] input, int opCodeIndex)
        {
            int opCode = input[opCodeIndex];

            int[] output = null;

            switch (opCode)
            {
                case 1:
                    output = Execute(input, opCodeIndex, (a, b) => a + b);
                    break;
                case 2:
                    output = Execute(input, opCodeIndex, (a, b) => a * b);
                    break;
                case 99:
                    break;  // program halted
                default:
                    throw new InvalidOperationException($"Invalid opcode: {opCode}");
            }

            if (output == null)
            {
                return input;
            }
            else
            {
                return Execute(output, opCodeIndex + 4);
            }
        }

        private static int[] Execute(int[] input, int opCodeIndex, Func<int, int, int> calculator)
        {
            int firsArgIndex = input[opCodeIndex + 1];
            int secondArgIndex = input[opCodeIndex + 2];
            int resultIndex = input[opCodeIndex + 3];

            int result = calculator(input[firsArgIndex], input[secondArgIndex]);
            input[resultIndex] = result;

            return input.ToArray();
        }
    }
}
