using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode._2019.Day11
{
    class Program
    {
        enum PanelColor { Black = 0, White = 1 }
        enum Direction { Left = 0, Right = 1, Up = 2, Down = 3 }

        static Direction TurnRobot(Direction robotDirection, Direction directionToTurnOn)
        {
            switch (directionToTurnOn)
            {
                case Direction.Left:
                    switch (robotDirection)
                    {
                        case Direction.Left:
                            return Direction.Down;
                        case Direction.Right:
                            return Direction.Up;
                        case Direction.Up:
                            return Direction.Left;
                        case Direction.Down:
                            return Direction.Right;
                        default:
                            throw new ArgumentException($"Invalid robot direction: {robotDirection}");
                    }                    
                case Direction.Right:
                    switch (robotDirection)
                    {
                        case Direction.Left:
                            return Direction.Up;
                        case Direction.Right:
                            return Direction.Down;
                        case Direction.Up:
                            return Direction.Right;
                        case Direction.Down:
                            return Direction.Left;
                        default:
                            throw new ArgumentException($"Invalid robot direction: {robotDirection}");
                    }                    
                default:
                    throw new ArgumentException($"Invalid turning direction: {directionToTurnOn}");
            }
        }

        static (long X, long Y) StepForwardRobot((long X, long Y) robotPosition, Direction robotDirection)
        {
            var newPosition = robotPosition;

            switch (robotDirection)
            {
                case Direction.Left:
                    newPosition.X--;
                    break;
                case Direction.Right:
                    newPosition.X++;
                    break;
                case Direction.Up:
                    newPosition.Y++;
                    break;
                case Direction.Down:
                    newPosition.Y--;
                    break;
            }

            return newPosition;
        }

        static void Main(string[] args)
        {
            var panels = new Dictionary<long, Dictionary<long, PanelColor>>();
            var robotPosition = (X: (long)0, Y: (long)0);
            var robotDirection = Direction.Up;

            long[] robotProgram = InputReader.Read();

            Func<long?> inputReader = () =>
            {                
                if (!panels.ContainsKey(robotPosition.X) || !panels[robotPosition.X].ContainsKey(robotPosition.Y))
                {
                    return (long)PanelColor.Black;
                }
                else
                {
                    return (long)panels[robotPosition.X][robotPosition.Y];
                }
            };

            var outputValues = new List<long>(2);
            Action<long> outputWriter = output =>
            {
                outputValues.Add(output);

                if (outputValues.Count == 2)
                {
                    PanelColor colorToPaint = (PanelColor)outputValues[0];
                    Direction directionToTurnOn = (Direction)outputValues[1];
                    outputValues.Clear();
                                        
                    // paint panel
                    if (!panels.ContainsKey(robotPosition.X))
                    {
                        panels[robotPosition.X] = new Dictionary<long, PanelColor>();
                    }
                    panels[robotPosition.X][robotPosition.Y] = colorToPaint;

                    // turn robot
                    robotDirection = TurnRobot(robotDirection, directionToTurnOn);

                    // step forward robot
                    robotPosition = StepForwardRobot(robotPosition, robotDirection);
                }
            };

            var robot = new IntcodeProgram(robotProgram, inputReader, outputWriter);
            robot.Execute();

            if (!robot.Halted)
            {
                throw new InvalidOperationException();
            }

            long panelsPaintedAtLeastOnce = panels.Keys.Sum(x => panels[x].Keys.Count);
            Console.WriteLine(panelsPaintedAtLeastOnce);
        }
    }
}
