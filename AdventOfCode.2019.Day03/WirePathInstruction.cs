using System;

namespace AdventOfCode._2019.Day03
{
    public enum WirePathInstructionDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class WirePathInstruction
    {
        public int StepCount { get; }

        public WirePathInstructionDirection Direction { get; }

        public WirePathInstruction(string wirePathInstructionString)
        {
            char stepDirection = wirePathInstructionString[0];
            switch (stepDirection)
            {
                case 'U':
                    Direction = WirePathInstructionDirection.Up;
                    break;
                case 'D':
                    Direction = WirePathInstructionDirection.Down;
                    break;
                case 'R':
                    Direction = WirePathInstructionDirection.Right;
                    break;
                case 'L':
                    Direction = WirePathInstructionDirection.Left;
                    break;
                default:
                    throw new ArgumentException($"Unkown direction: {stepDirection}");
            }
            StepCount = int.Parse(wirePathInstructionString.Substring(1, wirePathInstructionString.Length - 1));
        }
    }
}
