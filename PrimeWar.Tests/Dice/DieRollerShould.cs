using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Controllers;
using System.Collections.Generic;

namespace PrimeWar.Tests.Dice
{
    [TestFixture]
    public class DieRollerShould
    {
        [Test]
        public void ReturnCorrectNumberOfFaces()
        {
            DieCode dieCode = new DieCode(1, 2, 3);
            var sut = new DieRoller(dieCode);
            var resultList = sut.Roll();
            Assert.That(resultList.Count, Is.EqualTo(6), "Result List Count =" + resultList.Count);
        }

        [Test]
        public void ReturnAllGuaranteedFacesWithRoll()
        {
            List<DieFaces> guaranteed = new List<DieFaces>() {DieFaces.Support, DieFaces.Move };
            DieCode dieCode = new DieCode(1, 2, 3, guaranteed);
            var sut = new DieRoller(dieCode);
            Assert.That(guaranteed, Is.SubsetOf(sut.Roll()));
        }
    }
}
