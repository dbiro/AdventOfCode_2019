using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2019.Day03
{
    public static class WirePathIntersectionCalculator
    {
        public static IEnumerable<(int i, int j)> CalculateIntersectionPoints(IEnumerable<(int i, int j)> wire1Points, IEnumerable<(int i, int j)> wire2Points)
        {
            return wire1Points.Intersect(wire2Points).ToList();
        }

        public static int CalculateMinimumDistanceFromCentralPort(IEnumerable<(int i, int j)> wire1Points, IEnumerable<(int i, int j)> wire2Points)
        {
            return CalculateIntersectionPoints(wire1Points, wire2Points).Min(p => Math.Abs(p.i) + Math.Abs(p.j));
        }
    }
}
