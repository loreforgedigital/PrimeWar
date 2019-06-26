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
    public class AbilitiesInPlayManager
    {
        public List<IAction> ActionsInPlay;
        public Dictionary<TriggerTypes, List<IAction>> ActionsForTrigger;
        public void CardEntersPlay(ICard card)
        {
            foreach(var action in card.Actions)
            {
                var actionList = !ActionsForTrigger.ContainsKey(action.Trigger) ? new List<IAction>() : ActionsForTrigger[action.Trigger];
                actionList.Add(action);
                ActionsForTrigger[action.Trigger] = actionList;
            }
        }
        public List<IAction> GetActionsForTriggerType(TriggerTypes trigger)
        {
            return !ActionsForTrigger.ContainsKey(trigger) ? new List<IAction>() : ActionsForTrigger[trigger];
        }
    }

    public interface ICard
    {
        List<IAction> Actions {get;set;}
        int PrintedMeanwhile { get; set; }
        int EffectiveMeanwhile { get; set; }
        string Title { get; set; }
    }
    public interface IAction
    {
        TriggerTypes Trigger { get; set; }
        bool Request(string triggerId );
        //bool 
    }

    public class EventManager
    {
        KeyValuePair<string, List<IAction>> actionsForEvents = new KeyValuePair<string, List<IAction>>();
        public bool PublishEvent(string eventName, TriggerTypes trigger, object triggerDetails)
        {

            return false;
        }
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
        public IEnumerable<Coordinates> Action(IInputManager input, TargetController toMove, bool upTo = true)
        {
            return input.RequestMovementPath(toMove.Position, Distance);
        }
    }

    public interface IInputManager
    {
        IEnumerable<Coordinates> RequestMovementPath(Coordinates start, int distanceToMove, bool upTo = true);
    }

    

    public delegate IEnumerator GameControllerActionEvent(GameAction action);
}


