using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] inputData = File.ReadAllLines("input.txt");
            int[] inputMasses = inputData.Select(i => int.Parse(i.Trim())).ToArray();
            var masses = ImmutableList.Create(inputMasses);

            int sumOfFuelRequirements = 0;
            foreach (var mass in masses)
            {
                int fuelRequirement = mass / 3 - 2;
                sumOfFuelRequirements += fuelRequirement;
            }

            Console.WriteLine(sumOfFuelRequirements);
        }
    }
}
