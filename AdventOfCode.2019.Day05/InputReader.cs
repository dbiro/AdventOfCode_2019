using System;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day05
{
    public static class InputReader
    {
        private const string inputFile = "input.txt";

        public static int[] Read()
        {
            string inputData = File.ReadAllText(inputFile);
            return inputData.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(d => int.Parse(d)).ToArray();
        }
    }
}
