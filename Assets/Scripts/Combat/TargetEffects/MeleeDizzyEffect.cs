namespace BlueRacconGames.MeleeCombat
{
    public class MeleeDizzyEffect : IMeleeWeaponTargetEffect
    {
        private float dizzyTime;

        public MeleeDizzyEffect(MeleeDizzyEffectFactorySO initialData)
        {
            dizzyTime = initialData.DizzyTime;
        }

        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            MeleeCombatControllerBase meleeCombatController = target.GameObject.GetComponent<MeleeCombatControllerBase>();

            meleeCombatController.Dizzy(dizzyTime);
        }
    }
}
