using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Dice;
using PrimeWarEngine.Domain.Components.Abilities;
using PrimeWarEngine.Domain.Components.Map;
using PrimeWarEngine.ConsoleHelpers;

namespace PrimeWarEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            MovementTest();
            
          
        }

        static void MovementTest()
        {
            Random random = new Random();
            List<Coordinates> relativePath = new List<Coordinates>();
            var totalCoordinates = MapMath.GetCoordinatesForScenarioMap();
            TargetController testController = new TargetController();
            testController.Position = totalCoordinates[random.Next(0, totalCoordinates.Count)];
            ConsoleMapPrinter.printBoardWithTargets(new List<TargetController>(){ testController});
            Stack<Coordinates> targetPath = new Stack<Coordinates>();
            targetPath.Push(testController.Position);
            bool continueTravel = true;
            while (continueTravel)
            {
                ConsoleMapPrinter.printBoardWithPath(targetPath);
                Console.WriteLine("Add to your path? Y/N");
                string continueResponse = Console.ReadLine();
                continueTravel = !(continueResponse.Contains("N") || continueResponse.Contains("n"));
                if (continueTravel)
                {
                    var nextCoord = ConsoleOptions<Coordinates>.GetResponseToOptions(MapMath.relativeAdjacents.ToList(), MapMath.relativeAdjacentsReadable);
                    relativePath.Add(nextCoord);
                    targetPath.Push(nextCoord + targetPath.Peek());
                }
            }
            MovementController mc = new MovementController();
            mc.MoveAlongPath(testController, relativePath);
            ConsoleMapPrinter.printBoardWithTargets(new List<TargetController>() { testController });
            Console.ReadLine();

        }
        void AttackTest()
        {
            Random random = new Random();

            //Build relevant lists
            var randCoords = MapMath.RandomUniqueValidCoordinates(6);
            TargetController playerController = new TargetController(new Target("Player", 8), randCoords[0], new DieCode(random));
            List<TargetController> targets = new List<TargetController>()
            {
                new TargetController(new Target("Targ A", random.Next(1, 8)), randCoords[1], new DieCode(random)),
                new TargetController(new Target("Targ B", random.Next(1, 8)), randCoords[2], new DieCode(random)),
                new TargetController(new Target("Targ C", random.Next(1, 8)), randCoords[3], new DieCode(random)),
                new TargetController(new Target("Targ D", random.Next(1, 8)), randCoords[4], new DieCode(random)),
                new TargetController(new Target("Targ E", random.Next(1, 8)), randCoords[5], new DieCode(random))
            };
            List<AttackCode> attacks = new List<AttackCode>()
            {
                new AttackCode(new DieCode(random), random.Next(1, 8), false),
                new AttackCode(new DieCode(random), random.Next(1, 8), false),
                new AttackCode(new DieCode(random), random.Next(1, 8), false),
                new AttackCode(new DieCode(random), random.Next(1, 8), false),
                new AttackCode(new DieCode(random), random.Next(1, 8), false),
                new AttackCode(new DieCode(random), random.Next(1, 8), false)
            };
            var totalTargets = targets.ToList();
            totalTargets.Add(playerController);
            ConsoleMapPrinter.printBoardWithTargets(totalTargets);

            ////Display and choose

            AttackCode selectedAttack = ConsoleOptions<AttackCode>.GetResponseToOptions(attacks);

            List<TargetController> targetsInRange = targets.Where(t => MapMath.DistanceBetween(t.Position, playerController.Position) <= selectedAttack.Range).ToList();

            TargetController selectedTarget = ConsoleOptions<TargetController>.GetResponseToOptions(targetsInRange);

            Attack IcicleFling = new Attack(selectedAttack);
            List<DieFaces> Resolution = IcicleFling.MakeAttack(playerController.Position, selectedTarget);
            Console.WriteLine("The Attacker rolled: ");
            Console.WriteLine(string.Join(",", Resolution));

            DieRoller rollboy = new DieRoller(selectedTarget.CurrentDefense);
            List<DieFaces> defenseroll = rollboy.Roll();
            Console.WriteLine("The Defender rolled: ");
            Console.WriteLine(string.Join(",", defenseroll));

            Console.Read();
        }
    }

    
}
