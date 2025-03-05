using Game.Item;
using Game.Item.Factory.Implementation;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeWeaponBase : ItemBase, IMeleeWeapon
    {
        private readonly HashSet<IDamagableTarget> hitTargets = new();

        public List<IMeleeWeaponTargetEffect> MeleeWeaponTargetHitEffects { get; }

        public event Action<IDamagableTarget> OnHitE;

        protected MeleeWeaponBase(MeleeWeaponBaseFactorySO initialData) : base(initialData)
        {
            MeleeWeaponTargetHitEffects = new();

            foreach (IMeleeWeaponTargetEffectFactory meleeWeaponTargetEffectFactory in initialData.MeleeWeaponTargetEffectFactory)
            {
                MeleeWeaponTargetHitEffects.Add(meleeWeaponTargetEffectFactory.CreateEffect());
            }
        }

        public void OnHit(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            if (hitTargets.Contains(target))
                return;

            hitTargets.Add(target);
            OnHitInternal(source, target);
        }

        public void ResetWeapon()
        {
            hitTargets.Clear();
        }

        private void OnHitInternal(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            OnHitE?.Invoke(target);

            foreach (IMeleeWeaponTargetEffect meleeWeaponTargetHitEffect in MeleeWeaponTargetHitEffects)
            {
                meleeWeaponTargetHitEffect.Execute(source, target);
            }
        }
    }
}