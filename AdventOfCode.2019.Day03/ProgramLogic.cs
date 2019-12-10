using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day03
{
    public static class ProgramLogic
    {
        public static int CalculateMinimumDistanceFromCentralPort(IEnumerable<(int i, int j)> wire1Points, IEnumerable<(int i, int j)> wire2Points)
        {
            return WirePathIntersectionCalculator.CalculateIntersectionPoints(wire1Points, wire2Points).Min(p => Math.Abs(p.i) + Math.Abs(p.j));
        }

        public static int CalculateFewestCombinedStepsFromIntersections(IEnumerable<(int i, int j)> wire1Points, IEnumerable<(int i, int j)> wire2Points)
        {
            return WirePathIntersectionCalculator.CalculateIntersectionPoints(wire1Points, wire2Points).Min(p => WirePathTracker.TrackWireToPoint(wire1Points, p) + WirePathTracker.TrackWireToPoint(wire2Points, p));
        }
    }
}
