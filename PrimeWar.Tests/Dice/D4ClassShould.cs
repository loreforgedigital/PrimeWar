using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using System.Collections.Generic;

namespace PrimeWar.Tests.Dice
{
    [TestFixture]
    public class D4ClassShould
    {
        [Test]
        public void HaveFourFaces()
        {
            var sut = new D4();
            Assert.That(sut.totalFaces, Has.Exactly(4).Items);
        }
        [Test]
        public void ReturnCorrectDieFaces()
        {
            IEnumerable<DieFaces> correctFaces = new List<DieFaces>() { DieFaces.Hit, DieFaces.Hit, DieFaces.Shift, DieFaces.Opportunity };
            var sut = new D4();
            Assert.That(sut.totalFaces, Is.EquivalentTo(correctFaces));
        }
    }
}
