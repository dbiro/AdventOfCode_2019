using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCode._2019.Day03.UnitTests
{
    [TestClass]
    public class WirePathIntersectionCalculatorTests
    {
        [TestMethod]
        public void TestCase_01()
        {
            var wire1Coordinates = WirePathTracker.TrackWire("R8,U5,L5,D3".Split(",", StringSplitOptions.RemoveEmptyEntries));
            var wire2Coordinates = WirePathTracker.TrackWire("U7,R6,D4,L4".Split(",", StringSplitOptions.RemoveEmptyEntries));

            int distanceToTheClosestIntersection = WirePathIntersectionCalculator.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(6, distanceToTheClosestIntersection);
        }

        [TestMethod]
        public void TestCase_02()
        {
            var wire1Coordinates = WirePathTracker.TrackWire("R75,D30,R83,U83,L12,D49,R71,U7,L72".Split(",", StringSplitOptions.RemoveEmptyEntries));
            var wire2Coordinates = WirePathTracker.TrackWire("U62,R66,U55,R34,D71,R55,D58,R83".Split(",", StringSplitOptions.RemoveEmptyEntries));

            int distanceToTheClosestIntersection = WirePathIntersectionCalculator.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(159, distanceToTheClosestIntersection);
        }

        [TestMethod]
        public void TestCase_03()
        {
            var wire1Coordinates = WirePathTracker.TrackWire("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51".Split(",", StringSplitOptions.RemoveEmptyEntries));
            var wire2Coordinates = WirePathTracker.TrackWire("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7".Split(",", StringSplitOptions.RemoveEmptyEntries));

            int distanceToTheClosestIntersection = WirePathIntersectionCalculator.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(135, distanceToTheClosestIntersection);
        }
    }
}
