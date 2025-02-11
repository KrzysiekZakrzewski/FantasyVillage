using BlueRacconGames.Animation;
using BlueRacconGames.Movement;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeCombatControllerBase : MonoBehaviour
    {
        [SerializeField]
        private Transform attackPoint;
        [SerializeField]
        private LayerMask hitLayer;
        [SerializeField]
        private float attackRange = 50f;

        protected UnitAnimationControllerBase animationController;
        protected List<GameObject> targetsGM = new();
        protected IMeleeWeapon weapon;

        private Vector2 lastHitPoint;

        protected virtual void Awake()
        {
            animationController = GetComponent<UnitAnimationControllerBase>();
        }

        public virtual void Attack()
        {
            if (!CanAttack())
                return;
        }

        public virtual void DamageDetected()
        {
            weapon.ResetWeapon();

            Collider2D[] hitedObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, hitLayer);

            if (hitedObjects.Length == 0)
                return;

            foreach (Collider2D hit in hitedObjects)
            {
                if (targetsGM.Contains(hit.gameObject))
                    continue;

                targetsGM.Add(hit.gameObject);

                if (!hit.TryGetComponent<IWeaponTarget>(out var target))
                    continue;

                weapon.OnHit(this, target);
                lastHitPoint = hit.transform.position;
            }
        }

        public virtual void Dizzy(float dizzyTime)
        {

        }

        public void SpawnHitEffect(ParticleSystem vfx)
        {
            Instantiate(vfx, lastHitPoint, Quaternion.identity, null);
        }

        protected abstract bool CanAttack();

        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;

            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}