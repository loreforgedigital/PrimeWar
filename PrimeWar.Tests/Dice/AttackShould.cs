using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Controllers;
using System;

namespace PrimeWar.Tests.Dice
{
    [TestFixture]
    public class AttackShould
    {
        [Test]
        public void ThrowExceptionWhenTargetOutOfRange()
        {
            DieCode dieCode = new DieCode(1, 2, 3);
            Target t = new Target("test", 7, dieCode);
            var sut = new Attack(new PrimeWarEngine.Domain.Components.Abilities.AttackCode(dieCode, 4, false));
            Assert.That(() => sut.MakeAttack(t), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
