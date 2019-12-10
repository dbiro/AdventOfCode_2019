using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day03
{
    class Program
    {
        

        static void Main(string[] args)
        {
            string[] wireLines = File.ReadAllLines("input.txt");

            IEnumerable<string> wire1Path = wireLines[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> wire2Path = wireLines[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            var wire1Coordinates = WirePathTracker.TrackWire(wire1Path);
            var wire2Coordinates = WirePathTracker.TrackWire(wire2Path);
                        
            int distanceToTheClosestIntersection = WirePathIntersectionCalculator.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Console.WriteLine(distanceToTheClosestIntersection);
        }
    }
}
