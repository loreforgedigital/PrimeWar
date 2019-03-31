using System.Collections.Generic;
using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWar.Tests.Map
{
    [TestFixture]
    public class MapMathShould
    {
        [Test]
        public void CalculateDistanceToAdjacentCoordinate()
        {
            var distance = MapMath.DistanceBetween(new Coordinates(0,0), new Coordinates(1, 0));
            Assert.That(distance, Is.EqualTo(1));
        }
        [Test]
        public void HaveCorrectAdjacentCoordinates()
        {
            List<Coordinates> correctAdjacentCoordinates = new List<Coordinates>() {
                new Coordinates(1, 0), //SouthEast
                new Coordinates(1, -1),//NorthEast
                new Coordinates(0, -1),//North
                new Coordinates(-1, 0),//NorthWest
                new Coordinates(-1, 1),//SouthWest
                new Coordinates(0, 1)//South
            };
            Assert.That(MapMath.relativeAdjacents, Is.EquivalentTo(correctAdjacentCoordinates));
        }
        [Test]
        [TestCase(0,0,-3,4, ExpectedResult =4)]
        [TestCase(-3, 0, -3, 4, ExpectedResult = 4)]
        [TestCase(1, -4, -3, 4, ExpectedResult = 8)]
        [TestCase(0, 2, -3, 4, ExpectedResult = 3)]
        public int CalculateDistanceCorrectly(int aq, int ar, int bq, int br)
        {
            return MapMath.DistanceBetween(new Coordinates(aq, ar), new Coordinates(bq, br));
        }
        [Test]
        public void RotateSetOfCoordinates()
        {
            var originalPoints = new List<Coordinates>() { new Coordinates(0, 1), new Coordinates(1, 0),  new Coordinates(0, 2), new Coordinates(1, 1), new Coordinates(2, 0) };
            var rotatedPoints = new List<Coordinates>() { new Coordinates(1, -1), new Coordinates(0, -1),  new Coordinates(2, -2), new Coordinates(1, -2), new Coordinates(0, -2) };
            Assert.That(MapMath.RotateCoordinates(originalPoints, false, 2), Is.EquivalentTo(rotatedPoints));
        }

        [Test]
        public void CreateShapesCorrectly()
        {
            Assert.That(MapMath.GetShape(Shapes.Cone, 3), Has.Exactly(10).Items);
            Assert.That(MapMath.GetShape(Shapes.Radius, 3), Has.Exactly(37).Items);
            Assert.That(MapMath.GetShape(Shapes.Sweep, 2), Has.Exactly(12).Items);
            Assert.That(MapMath.GetShape(Shapes.Beam, 3), Has.Exactly(10).Items);
        }
    }
}
