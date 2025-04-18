using BlueRacconGames.Hit;
using BlueRacconGames.HitEffect.Factory;

namespace BlueRacconGames.HitEffect
{
    public class MeleeDizzyEffect : IHitTargetEffect
    {
        private float dizzyTime;

        public MeleeDizzyEffect(MeleeDizzyEffectFactorySO initialData)
        {
            dizzyTime = initialData.DizzyTime;
        }

        public void Execute(HitObjectControllerBase source, IHitTarget target)
        {
            HitObjectControllerBase meleeCombatController = target.GameObject.GetComponent<HitObjectControllerBase>();

            //meleeCombatController.Dizzy(dizzyTime);
        }
    }
}
