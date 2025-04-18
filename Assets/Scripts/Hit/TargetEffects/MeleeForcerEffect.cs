using BlueRacconGames.Hit;
using UnityEngine;

namespace BlueRacconGames.HitEffect
{
    public class MeleeForcerEffect : IHitTargetEffect
    {
        private Vector2 forceValue;
        
        public MeleeForcerEffect(Vector2 forceValue)
        {
            this.forceValue = forceValue;
        }

        public void Execute(HitObjectControllerBase source, IHitTarget target)
        {
            var rb = source.GetComponent<Rigidbody2D>();

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(forceValue);
        }
    }
}