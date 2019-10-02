using System.Collections.Generic;
using PrimeWarEngine.Domain.Components.Map;
using PrimeWarEngine.Domain.Components.Dice;
using System.Linq;

namespace PrimeWarEngine.Domain.Controllers
{
    public class SuccessController
    {
        public IEnumerable<DieFaces> facesForEvaluation;
        public IEnumerable<TargetController> allies;
        public Hex originTargetHex;
        public bool cannotHaveSupport = false;
        public bool cannotHaveOpportunity = false;
        public bool hasSupport = false;
        public bool hasOpportunity = false;

        public SuccessController(IEnumerable<DieFaces> faces, IEnumerable<TargetController> allyTargets, Hex originHex, bool overrideHasSupport = false, bool overrideHasOpportunity = false, bool overrideCannotHaveSupport = false, bool overrideCannotHaveOpportunity = false)
        {
            facesForEvaluation = faces;
            allies = allyTargets;
            originTargetHex = originHex;
            cannotHaveOpportunity = overrideCannotHaveOpportunity;
            cannotHaveSupport = overrideCannotHaveSupport;
            hasOpportunity = overrideHasOpportunity;
            hasSupport = overrideHasSupport;
        }

        public SuccessResult CalculateSuccess()
        {
            int successes = 0;
            int vitalSuccesses = 0;
            successes += facesForEvaluation.Count(f => f == DieFaces.Hit);
            vitalSuccesses += facesForEvaluation.Count(f => f == DieFaces.Vital);
            if (!cannotHaveOpportunity && (originTargetHex.terrain != FeatureType.None || hasOpportunity))
            {
                successes += facesForEvaluation.Count(f => f == DieFaces.Opportunity);
                vitalSuccesses += facesForEvaluation.Count(f => f == DieFaces.OpportunityVital);
            }
            if (!cannotHaveSupport && (hasSupport || allies.Any(a => MapMath.DistanceBetween(a.Position, originTargetHex.coordinates) == 1)))
            {
                successes += facesForEvaluation.Count(f => f == DieFaces.Support);
                vitalSuccesses += facesForEvaluation.Count(f => f == DieFaces.SupportVital);
            }
            return new SuccessResult(successes, vitalSuccesses);
        }
    }
}


