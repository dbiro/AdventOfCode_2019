using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AdventOfCode._2019.Day03.UnitTests
{
    [TestClass]
    public class WirePathTrackerTests
    {
        [TestMethod]
        public void TrackWireToPoint_TestCase_01()
        {            
            var wire1Coordinates = WirePathTracker.TrackWire("R8,U5,L5,D3".Split(",", StringSplitOptions.RemoveEmptyEntries));
            var wire2Coordinates = WirePathTracker.TrackWire("U7,R6,D4,L4".Split(",", StringSplitOptions.RemoveEmptyEntries));

            var intersectionPoints = WirePathIntersectionCalculator.CalculateIntersectionPoints(wire1Coordinates, wire2Coordinates);

            int steps = WirePathTracker.TrackWireToPoint(wire1Coordinates, intersectionPoints[0]);
            Assert.AreEqual(15, steps);

            steps = WirePathTracker.TrackWireToPoint(wire1Coordinates, intersectionPoints[1]);            
            Assert.AreEqual(20, steps);
        }
    }
}
