using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Components.Dice;

namespace PrimeWarEngine.Domain.Components.Abilities
{
    public class AttackCode
    {
        public DieCode DieCode;
        public int Range;
        bool Melee;
        public AttackCode(DieCode x,int Range,bool Melee)
        {
            DieCode = x;
            this.Range = Range;
            this.Melee = Melee;
        }

        public override string ToString()
        {
            return "Attack: " + DieCode + " at range " + Range;
        }

    }

}
