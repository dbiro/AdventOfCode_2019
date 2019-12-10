using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day03
{
    public static class WirePathIntersectionCalculator
    {        
        public static List<(int i, int j)> CalculateIntersectionPoints(IEnumerable<(int i, int j)> wire1Points, IEnumerable<(int i, int j)> wire2Points)
        {
            return wire1Points.Intersect(wire2Points).ToList();
        }
    }
}
