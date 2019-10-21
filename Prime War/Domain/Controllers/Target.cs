using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.Domain.Controllers
{
    public class Target
    {
        public string Name;
        public int MaxHealth;
        public Target()
        {
            this.Name = "UNNAMEDTARGET";
            this.MaxHealth = 6;
        }
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
        public TargetController()
        {
            Target = new Target();
            Position = Coordinates.origin;
            CurrentHealth = Target.MaxHealth;
            CurrentDefense = new DieCode();
        }
        public TargetController(Target t, Coordinates start, DieCode defaultDefense = null)
        {
            Target = t;
            Position = start;
            CurrentHealth = Target.MaxHealth;
            CurrentDefense = defaultDefense;
        }
        public bool EnterHex(Coordinates relativePosition)
        {
            //FireExitHexEvent(this, Position); EventHandlerShould fire Would and consumeAll, then fire Did, then consumeAll
            Position += relativePosition;
            //FireEnterHexEvent(this, Position);
            return true;
        }

        public override string ToString()
        {
            return Target.Name + ": " + CurrentHealth + " Health, defending with: " + CurrentDefense;
        }
    }
}
