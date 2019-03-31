using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Components.Abilities;
using PrimeWarEngine.Domain.Components.Dice;

namespace PrimeWarEngine.Domain.Controllers
{
    public class Attack
    {
        AttackCode code;
        public Attack(AttackCode punch)
        {
            code = punch;
        }
        public List<DieFaces> MakeAttack(Target T)
        {
            if (T.Range <= code.Range) //if target is within range
            {
                DieRoller dieRoller = new DieRoller(code.DieCode);
                return dieRoller.Roll();
            }
            else
                throw new ArgumentOutOfRangeException("target");
        }
    }
}
