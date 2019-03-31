using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.Domain.Controllers
{
    public class Target
    {
        public string Name;
        public int Range;
        public DieCode Defense;
        public Target(string Name, int Range, DieCode Defense)
        {
            this.Name = Name;
            this.Range = Range;
            this.Defense = Defense; 
        }
        public override string ToString()
        {
            return "Target " + Name + " is " + Range + " hexes away and defends with " + Defense;
        }
    }
    public class TargetController
    {
        public Coordinates Position;
        public Target Target;
        public TargetController(Target t, Coordinates start)
        {
            Target = t;
            Position = start;
        }
    }
}
