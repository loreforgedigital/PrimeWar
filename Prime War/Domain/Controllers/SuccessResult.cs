namespace PrimeWarEngine.Domain.Controllers
{
    public struct SuccessResult
    {
        public int Successes { get; }
        public int VitalSuccesses { get; }

        public SuccessResult(int success, int vitals)
        {
            Successes = success;
            VitalSuccesses = vitals;
        }

        public static SuccessResult operator -(SuccessResult attack, SuccessResult defense)
        {
            return new SuccessResult(
                attack.Successes > defense.Successes ? attack.Successes - defense.Successes : 0,
                attack.VitalSuccesses > defense.VitalSuccesses ? attack.VitalSuccesses - defense.VitalSuccesses : 0);
        }            
    }
}


