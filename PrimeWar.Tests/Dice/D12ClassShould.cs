using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using System.Collections.Generic;

namespace PrimeWar.Tests.Dice
{
    [TestFixture]
    public class D12ClassShould
    {
        [Test]
        public void HaveTwelveFaces()
        {
            var sut = new D12();
            Assert.That(sut.totalFaces, Has.Exactly(12).Items);
        }
        [Test]
        public void ReturnCorrectDieFaces()
        {
            IEnumerable<DieFaces> correctFaces = new List<DieFaces>() {
                DieFaces.Hit,
                DieFaces.Vital,
                DieFaces.Vital,
                DieFaces.Vital,
                DieFaces.Vital,
                DieFaces.Opportunity,
                DieFaces.Opportunity,
                DieFaces.Support,
                DieFaces.Support,
                DieFaces.Support,
                DieFaces.OpportunityVital,
                DieFaces.SupportVital
            };
            var sut = new D12();
            Assert.That(sut.totalFaces, Is.EquivalentTo(correctFaces));
        }
    }
}
