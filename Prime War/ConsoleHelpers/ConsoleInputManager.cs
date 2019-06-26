using System.Collections.Generic;
using System.Linq;
using PrimeWarEngine.Domain.Controllers;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.ConsoleHelpers
{
    public class ConsoleInputManager : IInputManager
    {
        public IEnumerable<Coordinates> RequestMovementPath(Coordinates start, int distanceToMove, bool upTo = true)
        {
            int hexesMoved = 0;
            bool finishedMoving = false;
            var totalCoordinates = MapMath.GetCoordinatesForScenarioMap();
            Stack<Coordinates> movementPath = new Stack<Coordinates>();
            List<Coordinates> options = (start + MapMath.relativeAdjacents).ToList();
            ConsoleMapPrinter.printBoardWithPath(movementPath);
            int optionIndex = ConsoleOptions<Coordinates>.GetResponseIndexOrDoneToOptions(options);
            while (hexesMoved<distanceToMove && !finishedMoving)
            {
                if (optionIndex == 6)
                {
                    finishedMoving = true;
                }
                else
                {
                    ConsoleMapPrinter.printBoardWithPath(movementPath);
                    var lastHexMoved = options[optionIndex];
                    if(movementPath.Contains(lastHexMoved))
                    {
                        while(movementPath.Count >=0 && movementPath.Peek()!=lastHexMoved)
                        {
                            movementPath.Pop();
                        }
                    }
                    else
                    movementPath.Push(lastHexMoved);
                    options = (lastHexMoved + MapMath.relativeAdjacents).ToList();
                    optionIndex = ConsoleOptions<Coordinates>.GetResponseIndexOrDoneToOptions(options);
                }
            }
            return movementPath;
        }
    }

    
}
