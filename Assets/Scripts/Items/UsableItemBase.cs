using BlueRacconGames.Animation;
using BlueRacconGames.MeleeCombat;
using Game.Item.Factory;
using UnityEngine;

namespace Game.Item
{
    public abstract class UsableItemBase : ItemBase
    {
        protected bool inUse;

        protected UsableItemBase(ItemFactorySO initialData) : base(initialData)
        {

        }

        protected abstract bool CanUse();

        public abstract bool Use(UnitAnimationControllerBase unitAnimation);
    }
}
