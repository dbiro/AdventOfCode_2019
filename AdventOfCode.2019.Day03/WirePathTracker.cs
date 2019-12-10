using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode._2019.Day03
{
    public static class WirePathTracker
    {        
        public static int TrackWireToPoint(IEnumerable<(int i, int j)> wirePoints, (int i, int j) point)
        {
            int stepCount = 0;
            foreach (var p in wirePoints)
            {
                stepCount++;
                if (p.i == point.i && p.j == point.j)
                {
                    break;
                }
            }
            return stepCount;
        }

        public static List<(int, int)> TrackWire(IEnumerable<string> wirePath)
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
    }
}
