using BlueRacconGames.Hit;
using BlueRacconGames.HitEffect;
using BlueRacconGames.HitEffect.Factory;
using Game.Item;
using Game.Item.Factory.Implementation;
using System;
using System.Collections.Generic;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeWeaponBase : ItemBase, IMeleeWeapon
    {
        private readonly HashSet<IHitTarget> hitTargets = new();

        public List<IHitTargetEffect> MeleeWeaponTargetHitEffects { get; }

        public event Action<IHitTarget> OnHitE;

        protected MeleeWeaponBase(MeleeWeaponBaseFactorySO initialData) : base(initialData)
        {
            MeleeWeaponTargetHitEffects = new();

            foreach (IHitTargetEffectFactory meleeWeaponTargetEffectFactory in initialData.MeleeWeaponTargetEffectFactory)
            {
                MeleeWeaponTargetHitEffects.Add(meleeWeaponTargetEffectFactory.CreateEffect());
            }
        }

        public void OnHit(HitObjectControllerBase source, IHitTarget target)
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

        private void OnHitInternal(HitObjectControllerBase source, IHitTarget target)
        {
            OnHitE?.Invoke(target);

            foreach (IHitTargetEffect meleeWeaponTargetHitEffect in MeleeWeaponTargetHitEffects)
            {
                meleeWeaponTargetHitEffect.Execute(source, target);
            }
        }
    }
}