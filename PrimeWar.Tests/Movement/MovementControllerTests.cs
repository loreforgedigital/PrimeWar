using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.Tests.Movement
{
    [TestFixture]
    public class MovementControllerTests
    {
        [Test]
        public void MoveAlongPath_Should_Move_TargetController_To_End_Of_Path()
        {
            TargetController tc = new TargetController();
            MovementController sut = new MovementController();
            var relativeMovements = new List<Coordinates>() { MapMath.relativeAdjacents[0] };
            sut.MoveAlongPath(tc, relativeMovements);
            Assert.AreEqual(MapMath.relativeAdjacents[0], tc.Position);
        }
    }
}
