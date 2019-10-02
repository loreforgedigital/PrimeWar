using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Map;
using System;

namespace PrimeWarEngine.Tests.Dice
{
    [TestFixture]
    public class AttackShould
    {
        [Test]
        public void ThrowExceptionWhenTargetOutOfRange()
        {
            DieCode dieCode = new DieCode(1, 2, 3);
            TargetController t = new TargetController( new Target("test", 7), new Coordinates(7,0));
            var sut = new Attack(new Domain.Components.Abilities.AttackCode(dieCode, 4, false));
            Assert.That(() => sut.MakeAttack( new Coordinates(), t), Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}
