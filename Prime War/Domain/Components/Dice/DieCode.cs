using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeWarEngine.Domain.Components.Dice
{
    public class DieCode
    {
        public int d4;

        public int d8;

        public int d12;

        public List<DieFaces> guaranteedFaces;
        public DieCode()
        {
            this.d4 = 0;
            this.d8 = 0;
            this.d12 = 0;
            this.guaranteedFaces = new List<DieFaces>();
        }
        public DieCode(int d4, int d8, int d12, List<DieFaces> guranteed = null)
        {
            this.d4 = d4;
            this.d8 = d8;
            this.d12 = d12;
            this.guaranteedFaces = guranteed != null ? guranteed : new List<DieFaces>();
        }
        public DieCode(Random random)
        {
            this.d4 = random.Next(0, 4);
            this.d8 = random.Next(0, 4);
            this.d12 = random.Next(0, 4);
            guaranteedFaces = new List<DieFaces>();
            int guaranteedLength = random.Next(-2, 4);
            for (int i = 0; i < guaranteedLength; i++)
            {
                guaranteedFaces.Add((DieFaces)Enum.GetValues(typeof(DieFaces)).GetValue(random.Next(8)));
            }
        }
        public override string ToString()
        {
            return d4 + "d4s," + d8 + "d8s," + d12 + "d12s" + guaranteedFaces != null && guaranteedFaces.Count > 0 ? ", " + string.Join(", ",guaranteedFaces) : "";
        }
    }
}
