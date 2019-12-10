using System;

namespace AdventOfCode._2019.Day03
{
    public enum WirePathSegmentDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class WirePathSegment
    {
        public int StepCount { get; }

        public WirePathSegmentDirection Direction { get; }

        public WirePathSegment(string wirePathSegment)
        {
            char stepDirection = wirePathSegment[0];
            switch (stepDirection)
            {
                case 'U':
                    Direction = WirePathSegmentDirection.Up;
                    break;
                case 'D':
                    Direction = WirePathSegmentDirection.Down;
                    break;
                case 'R':
                    Direction = WirePathSegmentDirection.Right;
                    break;
                case 'L':
                    Direction = WirePathSegmentDirection.Left;
                    break;
                default:
                    throw new ArgumentException($"Unkown direction: {stepDirection}");
            }
            StepCount = int.Parse(wirePathSegment.Substring(1, wirePathSegment.Length - 1));
        }
    }
}
