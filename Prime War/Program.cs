using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Components.Abilities;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Build relevant lists
            Random random = new Random();
            DieCode Numbah1 = new DieCode(1, 2, 3);
            Console.WriteLine(Numbah1);
            List<Target> availableTargets = new List<Target>()
            {
                new Target("A", 2, new DieCode( random.Next(0, 4),  random.Next(0, 4),  random.Next(0, 4))),
                new Target("B", 4, new DieCode( random.Next(0, 4),  random.Next(0, 4),  random.Next(0, 4))),
                new Target("C", 6, new DieCode( random.Next(0, 4),  random.Next(0, 4),  random.Next(0, 4))),
                new Target("D", 8, new DieCode( random.Next(0, 4),  random.Next(0, 4),  random.Next(0, 4)))
            };

            List<AttackCode> attacks = new List<AttackCode>()
            {
                new AttackCode(Numbah1, random.Next(1, 8), false),
                new AttackCode(Numbah1, random.Next(1, 8), false),
                new AttackCode(Numbah1, random.Next(1, 8), false),
                new AttackCode(Numbah1, random.Next(1, 8), false),
                new AttackCode(Numbah1, random.Next(1, 8), false)
            };

            //Display and choose

            foreach(Target t in availableTargets)
            {
                Console.WriteLine(t);
            }

            AttackCode selectedAttack = ConsoleOptions<AttackCode>.GetResponseToOptions(attacks);

            List<Target> targetsInRange = availableTargets.Where(t => t.Range <= selectedAttack.Range).ToList();

            Target selectedTarget = ConsoleOptions<Target>.GetResponseToOptions(targetsInRange);

            Attack IcicleFling = new Attack(selectedAttack);
            List<DieFaces>Resolution = IcicleFling.MakeAttack(selectedTarget);
            Console.WriteLine("The Attacker rolled: ");
            Console.WriteLine(string.Join(",", Resolution));

            DieRoller rollboy = new DieRoller(selectedTarget.Defense);
            List<DieFaces> defenseroll = rollboy.Roll();
            Console.WriteLine("The Defender rolled: ");
            Console.WriteLine(string.Join(",", defenseroll));

            Console.Read();
            
          
        }
    }

        public class ConsoleOptions<T>
        {
            public static T GetResponseToOptions(List<T> options)
            {
                Console.WriteLine("Please Select an option");
                for(int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine((i + 1) + ") " + options[i]);
                }
                string response = Console.ReadLine();
            var result = options.FirstOrDefault(o => o.ToString() == response);
                if (result != null)
                {
                    return result;
                }
                else if (int.TryParse(response, out int x) && x >=1 && x <= options.Count)
                {
                    return options[x-1];
                }
                else
                {
                    Console.WriteLine("Invalid Response. Please select an option by its name or number.");
                    return GetResponseToOptions(options);
                }
            }
        public static int GetResponseIndexOrDoneToOptions(List<T> options)
        {
            Console.WriteLine("Please Select an option");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine((i + 1) + ") " + options[i]);
            }
            Console.WriteLine((options.Count + 1) + ") Done"); 
            string response = Console.ReadLine();
            var result = options.FirstOrDefault(o => o.ToString() == response);
            if (result != null)
            {
                return options.IndexOf(result);
            }
            else if (int.TryParse(response, out int x) && x >= 1 && x <= options.Count+1)
            {
                return x - 1;
            }
            else
            {
                Console.WriteLine("Invalid Response. Please select an option by its name or number.");
                return GetResponseIndexOrDoneToOptions(options);
            }
        }
    }

    public class ConsoleInputManager : IInputManager
    {
        public List<Coordinates> RequestMovementPath(Coordinates start, int distanceToMove, bool upTo = true)
        {
            int hexesMoved = 0;
            bool finishedMoving = false;
            List<Coordinates> path = new List<Coordinates>();
            List<Coordinates> options = (start + MapMath.relativeAdjacents).ToList();
            int optionIndex = ConsoleOptions<Coordinates>.GetResponseIndexOrDoneToOptions(options);
            while (hexesMoved<distanceToMove && !finishedMoving)
            {
                if (optionIndex == 6)
                {
                    finishedMoving = true;
                }
                else
                {
                    var lastHexMoved = options[optionIndex];
                    path.Add(lastHexMoved);
                    options = (lastHexMoved + MapMath.relativeAdjacents).ToList();
                    optionIndex = ConsoleOptions<Coordinates>.GetResponseIndexOrDoneToOptions(options);
                }
            }
            return path;
        }
    }

}
