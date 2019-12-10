using System;
using System.IO;

namespace AdventOfCode._2019.Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] wireLines = File.ReadAllLines("input.txt");

            var wire1Segments = WirePathParser.Parse(wireLines[0]);
            var wire2Segments = WirePathParser.Parse(wireLines[1]);

            var wire1Points = WirePathTracker.TrackWire(wire1Segments);
            var wire2Points = WirePathTracker.TrackWire(wire2Segments);
                        
            int distanceToTheClosestIntersection = ProgramLogic.CalculateMinimumDistanceFromCentralPort(wire1Points, wire2Points);
            Console.WriteLine(distanceToTheClosestIntersection);

            int fewestCombinedSteps = ProgramLogic.CalculateFewestCombinedStepsFromIntersections(wire1Points, wire2Points);
            Console.WriteLine(fewestCombinedSteps);            
        }
    }
}
