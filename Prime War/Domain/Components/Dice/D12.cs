using System.Collections.Generic;

namespace PrimeWarEngine.Domain.Components.Dice
{
    public class D12 : Die
    {

        public D12()
        {
            this.totalFaces = new List<DieFaces>() {
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
        }

        public override string ToString()
        {
            return "d12";
        }
    }
}
