using System.Collections.Generic;
using System.Collections;
using PrimeWarEngine.Domain.Components.Abilities;
using PrimeWarEngine.Domain.Components.Map;

namespace PrimeWarEngine.Domain.Controllers
{
    public class GameManager
    {
        public Coordinates SelectFinalCoordinatesForMove(TargetController t)
        {
            Coordinates userPromptedCoordinates = t.Position + new Coordinates(3, 4);
            t.Position = userPromptedCoordinates;
            return userPromptedCoordinates;
        }
    }
    
    public interface IAction
    {
        TriggerTypes Trigger { get; set; }
        bool Request(string triggerId );
        //bool 
    }

    public class EventManager
    {
        
    }

    public class GameEvent
    {
        public string Id { get; private set; }

        List<IAction> listeners;
        
    }

    public class GameAction : IAction
    {
        public TriggerTypes Trigger { get; set; }

        public bool Request(string eventId)
        {
            throw new System.NotImplementedException();
        }
    }

    public class AttackAction : GameAction
    {
        AttackCode BaseCode;
        public AttackAction(AttackCode code)
        {
            BaseCode = code;
        }


    }

    public class MoveAction : GameAction
    {
        public int Distance;
        public MoveAction(int hexesToMove)
        {
            Distance = hexesToMove;
        }
        public List<Coordinates> Action(IInputManager input, TargetController toMove, bool upTo = true)
        {
            return input.RequestMovementPath(toMove.Position, Distance);
        }
    }

    public interface IInputManager
    {
        List<Coordinates> RequestMovementPath(Coordinates start, int distanceToMove, bool upTo = true);
    }

    public delegate IEnumerator GameControllerActionEvent(GameAction action);
}
