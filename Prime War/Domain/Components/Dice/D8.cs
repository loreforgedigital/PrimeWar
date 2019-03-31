using System.Collections.Generic;

namespace PrimeWarEngine.Domain.Components.Dice
{
    public class D8 : Die
    {
        public D8()
        {
            this.totalFaces = new List<DieFaces>() {
                DieFaces.Hit,
                DieFaces.Hit,
                DieFaces.Hit,
                DieFaces.Hit,
                DieFaces.Shift,
                DieFaces.Opportunity,
                DieFaces.Support,
                DieFaces.Vital};
        }
        public override string ToString()
        {
            return "d8";
        }
    }
}
