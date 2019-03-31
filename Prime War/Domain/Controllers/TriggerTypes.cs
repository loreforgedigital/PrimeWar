namespace PrimeWarEngine.Domain.Controllers
{
    public enum TriggerTypes
    {
        Custom= -1,
        SelectPhaseBegan,
        SelectPhaseEnded,
        CardSelected,
        CardRevealed,
        ActionPhaseBegan,
        ActionPhaseEnded,
        MeanwhilePhaseBegan,
        MeanwhilePhaseEnded,
        CardEnteredMeanwhileArea,
        CardLeftMeanwhileArea,
        CardEnteredHand,
        CardLeftHand,
        CardEnteredMeanwhileSlot,
        CardLeftMeanwhileSlot,
        CardEnteredBoonSlot,
        CardLeftBoonSlot,
        CardEnteredSelectedSlot,
        CardLeftSelectedSlot,
        AttackResolved,
        AttackDeclared,
        AttackFailed,
        TargetEnteredHex,
        TargetExitedHex
    }
}
