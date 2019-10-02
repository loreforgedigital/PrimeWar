using System.Collections.Generic;

namespace PrimeWarEngine.Domain.Components.Dice
{
    public class D4 : Die
    {
        public D4()
        {
            this.totalFaces = new List<DieFaces>() { DieFaces.Hit, DieFaces.Hit, DieFaces.Move, DieFaces.Opportunity };
        }
        public override string ToString()
        {
            return "d4";
        }
    }
}
