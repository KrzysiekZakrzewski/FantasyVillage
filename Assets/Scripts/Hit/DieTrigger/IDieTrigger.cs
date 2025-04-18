namespace BlueRacconGames.MeleeCombat
{
    public interface IDieTrigger
    {
        bool IsTriggered { get; }
        void Trigger();
        void ResetTrigger();
    }
}