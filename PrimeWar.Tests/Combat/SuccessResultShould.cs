using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Map;
using System;

namespace PrimeWar.Tests.Combat
{
    [TestFixture]
    public class SuccessResultShould
    {
        [Test]
        public void ReturnZeroSuccessesWhenDefenseSuccessGreaterThanAttackSuccess()
        {
            SuccessResult attack = new SuccessResult(3, 0);
            SuccessResult defense = new SuccessResult(4, 0);
            var result = attack - defense;
            Assert.Zero(result.Successes);
        }

        [Test]
        public void ReturnZeroVitalSuccessesWhenDefenseVitalSuccessGreaterThanAttackVitalSuccess()
        {
            SuccessResult attack = new SuccessResult(0, 3);
            SuccessResult defense = new SuccessResult(0, 4);
            var result = attack - defense;
            Assert.Zero(result.VitalSuccesses);
        }

        [Test]
        public void ReturnNotZeroSuccessesWhenAttackSuccessGreaterThanDefenseSuccess()
        {
            SuccessResult attack = new SuccessResult(5, 0);
            SuccessResult defense = new SuccessResult(4, 0);
            var result = attack - defense;
            Assert.NotZero(result.Successes);
        }

        [Test]
        public void ReturnNotZeroVitalSuccessesWhenAttackVitalSuccessGreaterThanDefenseVitalSuccess()
        {
            SuccessResult attack = new SuccessResult(0, 5);
            SuccessResult defense = new SuccessResult(0, 4);
            var result = attack - defense;
            Assert.NotZero(result.VitalSuccesses);
        }

        [Test]
        public void ReturnDifferenceSuccessesWhenAttackSuccessGreaterThanDefenseSuccess()
        {
            SuccessResult attack = new SuccessResult(5, 0);
            SuccessResult defense = new SuccessResult(4, 0);
            var result = attack - defense;
            Assert.AreEqual(1,result.Successes);
        }

        [Test]
        public void ReturnDifferenceVitalSuccessesWhenAttackVitalSuccessGreaterThanDefenseVitalSuccess()
        {
            SuccessResult attack = new SuccessResult(0, 5);
            SuccessResult defense = new SuccessResult(0, 4);
            var result = attack - defense;
            Assert.AreEqual(1,result.VitalSuccesses);
        }

    }
}
