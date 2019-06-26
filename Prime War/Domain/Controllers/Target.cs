using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.Domain.Controllers
{
    public class Target
    {
        public string Name;
        public int MaxHealth;
        public Target(string Name, int MaxHealth)
        {
            this.Name = Name;
            this.MaxHealth = MaxHealth; 
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public class TargetController
    {
        public Coordinates Position;
        public Target Target;
        public int CurrentHealth;
        public DieCode CurrentDefense;
        public TargetController(Target t, Coordinates start, DieCode defaultDefense = null)
        {
            Target = t;
            Position = start;
            CurrentHealth = Target.MaxHealth;
            CurrentDefense = defaultDefense;
        }

        public override string ToString()
        {
            return Target.Name + ": " + CurrentHealth + " Health, defending with: " + CurrentDefense;
        }
    }
}
