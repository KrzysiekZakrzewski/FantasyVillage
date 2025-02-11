using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class MeleeForcerEffect : IMeleeWeaponTargetEffect
    {
        private Vector2 forceValue;
        
        public MeleeForcerEffect(Vector2 forceValue)
        {
            this.forceValue = forceValue;
        }

        public void Execute(MeleeCombatControllerBase source, IWeaponTarget target)
        {
            var rb = source.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(forceValue);
        }
    }
}