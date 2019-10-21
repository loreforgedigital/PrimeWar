using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Map;
using System;
using System.Collections.Generic;

namespace PrimeWar.Tests.Combat
{
    [TestFixture]
    public class SuccessControllerTests
    {
        List<DieFaces> allMoves = new List<DieFaces>() { DieFaces.Move, DieFaces.Move, DieFaces.Move };
        List<DieFaces> allHit = new List<DieFaces>() { DieFaces.Hit, DieFaces.Hit, DieFaces.Hit };
        List<DieFaces> allVital = new List<DieFaces>() { DieFaces.Vital, DieFaces.Vital, DieFaces.Vital };
        List<DieFaces> allSupport = new List<DieFaces>() { DieFaces.Support, DieFaces.Support, DieFaces.Support };
        List<DieFaces> allOpportunity = new List<DieFaces>() { DieFaces.Opportunity, DieFaces.Opportunity, DieFaces.Opportunity };
        List<DieFaces> conditionalVital = new List<DieFaces>() { DieFaces.OpportunityVital, DieFaces.SupportVital };

        List<TargetController> adjacentAlly = new List<TargetController>() { new TargetController(new Target("test", 3), MapMath.relativeAdjacents[1]) };
        [Test]
        public void CalculateSuccess_ShouldReturnNoSuccesses_WhenAllFacesAreMove()
        {
            SuccessController successController = new SuccessController(allMoves, adjacentAlly, new Hex());
            var result = successController.CalculateSuccess();
            Assert.Zero(result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnNoSuccesses_WhenAllFacesAreOpportunityAndNotInOpportunity()
        {
            SuccessController successController = new SuccessController(allOpportunity, adjacentAlly, new Hex());
            var result = successController.CalculateSuccess();
            Assert.Zero(result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnNoSuccesses_WhenAllFacesAreSupportAndNoAdjacentAlly()
        {
            SuccessController successController = new SuccessController(allSupport, new List<TargetController>(), new Hex());
            var result = successController.CalculateSuccess();
            Assert.Zero(result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }
        [Test]
        public void CalculateSuccess_ShouldReturnSuccesses_WhenAllFacesAreHit()
        {
            SuccessController successController = new SuccessController(allHit, adjacentAlly, new Hex());
            var result = successController.CalculateSuccess();
            Assert.AreEqual(3, result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }
        [Test]
        public void CalculateSuccess_ShouldReturnVitalSuccesses_WhenAllFacesAreVital()
        {
            SuccessController successController = new SuccessController(allVital, adjacentAlly, new Hex());
            var result = successController.CalculateSuccess();
            Assert.AreEqual(3, result.VitalSuccesses);
            Assert.Zero(result.Successes);
        }



        [Test]
        public void CalculateSuccess_ShouldReturnSuccesses_WhenAllFacesAreOpportunityAndInOpportunity()
        {
            SuccessController successController = new SuccessController(allOpportunity, adjacentAlly, new Hex(Coordinates.origin, FeatureType.HighGround));
            var result = successController.CalculateSuccess();
            Assert.AreEqual(3, result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnSuccesses_WhenAllFacesAreSupportAndAdjacentAlly()
        {
            SuccessController successController = new SuccessController(allSupport, adjacentAlly, new Hex());
            var result = successController.CalculateSuccess();
            Assert.AreEqual(3, result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }
        [Test]
        public void CalculateSuccess_ShouldReturnVitalSuccesses_WhenVitalOpportunityAndInOpportunity()
        {
            SuccessController successController = new SuccessController(conditionalVital, new List<TargetController>(), new Hex(Coordinates.origin, FeatureType.HighGround));
            var result = successController.CalculateSuccess();
            Assert.AreEqual(1, result.VitalSuccesses);
            Assert.Zero(result.Successes);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnVitalSuccesses_WhenVitalSupportAndAdjacentAlly()
        {
            SuccessController successController = new SuccessController(conditionalVital, adjacentAlly, new Hex());
            var result = successController.CalculateSuccess();
            Assert.AreEqual(1, result.VitalSuccesses);
            Assert.Zero(result.Successes);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnNoSuccesses_WhenCannotHaveOpportunity()
        {
            SuccessController successController = new SuccessController(allOpportunity, adjacentAlly, new Hex(Coordinates.origin, FeatureType.HighGround), false, false, false,true);
            var result = successController.CalculateSuccess();
            Assert.Zero(result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnNoSuccesses_WhenCannotHaveSupport()
        {
            SuccessController successController = new SuccessController(allSupport, adjacentAlly, new Hex(),false,false,true);
            var result = successController.CalculateSuccess();
            Assert.Zero(result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }
        [Test]
        public void CalculateSuccess_ShouldReturnSuccesses_WhenAllFacesAreOpportunityAndOverrideHasOpportunity()
        {
            SuccessController successController = new SuccessController(allOpportunity, adjacentAlly, new Hex(), false, true);
            var result = successController.CalculateSuccess();
            Assert.AreEqual(3, result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }

        [Test]
        public void CalculateSuccess_ShouldReturnSuccesses_WhenAllFacesAreSupportAndOverrideHasSupport()
        {
            SuccessController successController = new SuccessController(allSupport, new List<TargetController>(), new Hex(),true);
            var result = successController.CalculateSuccess();
            Assert.AreEqual(3, result.Successes);
            Assert.Zero(result.VitalSuccesses);
        }


    }
}
