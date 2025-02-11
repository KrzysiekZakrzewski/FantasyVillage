using BlueRacconGames.MeleeCombat;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item.Factory.Implementation
{
    public abstract class MeleeWeaponBaseFactorySO : ItemFactorySO
    {
        [field: SerializeField]
        public List<MeleeWeaponTargetEffectFactorySO> MeleeWeaponTargetEffectFactory {  get; set; }
        public abstract override IItemRuntimeLogic CreateItem();
    }
}