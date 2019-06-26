using System.Collections.Generic;
using NUnit.Framework;
using PrimeWarEngine.Domain.Components.Map;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Controllers;

namespace PrimeWar.Tests.Map
{
    [TestFixture]
    public class MovementActionShould
    {
        TargetController t = new TargetController(new Target("test", 8), Coordinates.origin);
        //MovementActionShould m = new MovementAction(Card Source, TargetController target)
        //public int TotalDamage(List<DieFaces> attackResult, List<DieFaces> defenseResult)
        //{
        //    int attackVitals = 0;
        //    int attackHits = 0;
        //    int defenseVitals = 0;
        //    int defenseHits = 0;

        //    int unblockedAtkVitals = 0;
        //    int unusedDefVitals = 0;

        //    return unblockedAtkVitals + (attackHits - (defenseHits + unusedDefVitals));
        //}
    }
}
