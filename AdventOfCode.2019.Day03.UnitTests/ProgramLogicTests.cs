using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCode._2019.Day03.UnitTests
{
    [TestClass]
    public class ProgramLogicTests
    {
        [TestMethod]
        public void TestCase_01()
        {
            var wire1Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("R8,U5,L5,D3"));
            var wire2Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("U7,R6,D4,L4"));

            int distanceToTheClosestIntersection = ProgramLogic.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(6, distanceToTheClosestIntersection);
        }

        [TestMethod]
        public void TestCase_02()
        {
            var wire1Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("R75,D30,R83,U83,L12,D49,R71,U7,L72"));
            var wire2Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("U62,R66,U55,R34,D71,R55,D58,R83"));

            int distanceToTheClosestIntersection = ProgramLogic.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(159, distanceToTheClosestIntersection);
        }

        [TestMethod]
        public void TestCase_03()
        {
            var wire1Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51"));
            var wire2Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"));

            int distanceToTheClosestIntersection = ProgramLogic.CalculateMinimumDistanceFromCentralPort(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(135, distanceToTheClosestIntersection);
        }

        [TestMethod]
        public void TestCase_04()
        {
            var wire1Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("R8,U5,L5,D3"));
            var wire2Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("U7,R6,D4,L4"));

            int steps = ProgramLogic.CalculateFewestCombinedStepsFromIntersections(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(30, steps);
        }

        [TestMethod]
        public void TestCase_05()
        {
            var wire1Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("R75,D30,R83,U83,L12,D49,R71,U7,L72"));
            var wire2Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("U62,R66,U55,R34,D71,R55,D58,R83"));

            int steps = ProgramLogic.CalculateFewestCombinedStepsFromIntersections(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(610, steps);
        }

        [TestMethod]
        public void TestCase_06()
        {
            var wire1Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51"));
            var wire2Coordinates = WirePathTracker.TrackWire(WirePathParser.Parse("U98,R91,D20,R16,D67,R40,U7,R15,U6,R7"));

            int steps = ProgramLogic.CalculateFewestCombinedStepsFromIntersections(wire1Coordinates, wire2Coordinates);
            Assert.AreEqual(410, steps);
        }
    }
}
