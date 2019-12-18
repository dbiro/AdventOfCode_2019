using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day13
{
    public static class InputReader
    {
        private const string inputFile = "input.txt";

        public static long[] Read()
        {
            string inputData = File.ReadAllText(inputFile);
            return inputData.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(d => long.Parse(d)).ToArray();
        }
    }
}
