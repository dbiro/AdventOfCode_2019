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

            int sumOfFuelRequirements = masses.Sum(m => FuelRequirementCalculator.Calculate(m));
            
            Console.WriteLine(sumOfFuelRequirements);
        }
    }
}
