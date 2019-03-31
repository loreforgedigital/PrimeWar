using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Components.Dice;


namespace PrimeWarEngine.Domain.Controllers
{
    public class DieRoller
    {
        DieCode code;
        public DieRoller(DieCode x)
        {
            code = x;
        }
        public List<DieFaces> Roll()
        {
            List<DieFaces> temp = new List<DieFaces>(code.guaranteedFaces);
            D4 d4 = new D4();
            D8 d8 = new D8();
            D12 d12 = new D12();
            for(int i = 0; i < code.d4; i++)
            {
                temp.Add(d4.roll());
                
            }
            for (int i = 0; i < code.d8; i++)
            {
                temp.Add(d8.roll());

            }
            for (int i = 0; i < code.d12; i++)
            {
                temp.Add(d12.roll());

            }
            return temp;
        }

        
    }
}
