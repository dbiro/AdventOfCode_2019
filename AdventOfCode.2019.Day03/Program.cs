using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode._2019.Day03
{
    class Program
    {
        static List<(int, int)> TrackWire(IEnumerable<string> wirePath)
        {
            List<(int, int)> coordinates = new List<(int, int)>();

            int i = 0, j = 0;

            foreach (var step in wirePath)
            {
                char stepDirection = step[0];
                int stepCount = int.Parse(step.Substring(1, step.Length - 1));
                int to = 0;

                switch (stepDirection)
                {
                    case 'R':
                        to = i + stepCount;
                        do
                        {
                            coordinates.Add(ValueTuple.Create(++i, j));
                        }
                        while (i < to);
                        break;
                    case 'L':
                        to = i - stepCount;
                        do
                        {
                            coordinates.Add(ValueTuple.Create(--i, j));
                        }
                        while (i > to);
                        break;
                    case 'U':
                        to = j + stepCount;
                        do
                        {
                            coordinates.Add(ValueTuple.Create(i, ++j));
                        }
                        while (j < to);
                        break;
                    case 'D':
                        to = j - stepCount;
                        do
                        {
                            coordinates.Add(ValueTuple.Create(i, --j));
                        }
                        while (j > to);
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return coordinates;
        }

        static void Main(string[] args)
        {
            string[] wireLines = File.ReadAllLines("input.txt");

            IEnumerable<string> wire1Path = wireLines[0].Split(",", StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> wire2Path = wireLines[1].Split(",", StringSplitOptions.RemoveEmptyEntries);

            var wire1Coordinates = TrackWire(wire1Path);
            var wire2Coordinates = TrackWire(wire2Path);

            // 6
            //var wire1Coordinates = TrackWire("R8,U5,L5,D3".Split(",", StringSplitOptions.RemoveEmptyEntries));
            //var wire2Coordinates = TrackWire("U7,R6,D4,L4".Split(",", StringSplitOptions.RemoveEmptyEntries));

            // 159
            //var wire1Coordinates = TrackWire("R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(",", StringSplitOptions.RemoveEmptyEntries));
            //var wire2Coordinates = TrackWire("U62,R66,U55,R34,D71,R55,D58,R83".Split(",", StringSplitOptions.RemoveEmptyEntries));

            // 135
            //var wire1Coordinates = TrackWire("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(",", StringSplitOptions.RemoveEmptyEntries));
            //var wire2Coordinates = TrackWire("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(",", StringSplitOptions.RemoveEmptyEntries));

            var intersectionPoints = wire1Coordinates.Intersect(wire2Coordinates);
            int distanceToTheClosestIntersection = intersectionPoints.Min(c => Math.Abs(c.Item1) + Math.Abs(c.Item2));
            Console.WriteLine(distanceToTheClosestIntersection);
        }
    }
}
