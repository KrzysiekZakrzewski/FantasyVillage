namespace BlueRacconGames.MeleeCombat
{
    public interface IMeleeWeaponTargetEffect
    {
        void Execute(MeleeCombatControllerBase source, IWeaponTarget target);
    }
}