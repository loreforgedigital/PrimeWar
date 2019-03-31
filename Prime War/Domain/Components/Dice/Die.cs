using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeWarEngine.Domain.Components.Dice
{
    public abstract class Die
    {
        Random randoRoller = new Random();
        public List<DieFaces> totalFaces;
        public DieFaces roll()
        {
            return totalFaces[randoRoller.Next(totalFaces.Count)];    
        }
        public string allFaces()
        {
            return string.Join(",", totalFaces);
        }
    }
}
