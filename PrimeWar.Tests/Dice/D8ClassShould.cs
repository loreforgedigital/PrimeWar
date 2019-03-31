using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using System.Collections.Generic;

namespace PrimeWar.Tests.Dice
{
    [TestFixture]
    public class D8ClassShould
    {
        [Test]
        public void HaveEightFaces()
        {
            var sut = new D8();
            Assert.That(sut.totalFaces, Has.Exactly(8).Items);
        }
        [Test]
        public void ReturnCorrectDieFaces()
        {
            IEnumerable<DieFaces> correctFaces = new List<DieFaces>() {
                DieFaces.Hit,
                DieFaces.Hit,
                DieFaces.Hit,
                DieFaces.Hit,
                DieFaces.Shift,
                DieFaces.Opportunity,
                DieFaces.Support,
                DieFaces.Vital};
            var sut = new D8();
            Assert.That(sut.totalFaces, Is.EquivalentTo(correctFaces));
        }
    }
}
