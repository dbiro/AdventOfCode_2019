using System;
using System.Collections.Generic;

namespace AdventOfCode._2019.Day03
{
    public static class WirePathTracker
    {        
        public static int TrackWireToPoint(IEnumerable<(int i, int j)> wirePoints, (int i, int j) point)
        {
            int stepCount = 0;
            foreach (var wp in wirePoints)
            {
                stepCount++;
                if (wp.i == point.i && wp.j == point.j)
                {
                    break;
                }
            }
            return stepCount;
        }

        public static List<(int, int)> TrackWire(IEnumerable<WirePathSegment> wirePath)
        {
            List<(int, int)> coordinates = new List<(int, int)>();

            int i = 0, j = 0;

            foreach (var segment in wirePath)
            {                
                int to = 0;

                switch (segment.Direction)
                {
                    case WirePathSegmentDirection.Right:
                        to = i + segment.StepCount;
                        while (i < to)
                        {
                            coordinates.Add(ValueTuple.Create(++i, j));
                        }
                        break;
                    case WirePathSegmentDirection.Left:
                        to = i - segment.StepCount;
                        while (i > to)
                        {
                            coordinates.Add(ValueTuple.Create(--i, j));
                        }
                        break;
                    case WirePathSegmentDirection.Up:
                        to = j + segment.StepCount;
                        while (j < to)
                        {
                            coordinates.Add(ValueTuple.Create(i, ++j));
                        }
                        break;
                    case WirePathSegmentDirection.Down:
                        to = j - segment.StepCount;
                        while (j > to)
                        {
                            coordinates.Add(ValueTuple.Create(i, --j));
                        }
                        break;
                    default:
                        throw new InvalidOperationException();
                }
            }

            return coordinates;
        }
    }
}
