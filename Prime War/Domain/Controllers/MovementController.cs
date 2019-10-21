using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.Domain.Controllers
{
    public class MovementController : IMovementController
    {
        public Coordinates MoveAlongPath(TargetController target, List<Coordinates> relativePath)
        {
            foreach(var c in relativePath)
            {
                target.EnterHex(c);
            }
            return target.Position;
        }
    }

    public enum MovementOverrides
    {
        CanEndInOccupiedHex,
        CannotEnterOccupiedHex,
        CannotEnterEnemyHex,
        CannotEnterAllyHex,
        CanEnterWalls,
        CannotEnterHighGround,
        CannotEnterCover,
        CannotEnterConcealment
    }

    public interface IMovementController
    {
        Coordinates MoveAlongPath(TargetController target, List<Coordinates> relativePath);
        
    }
}
