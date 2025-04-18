using Game.Item;
using Game.Item.Factory.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat.Implementation
{
    [CreateAssetMenu(fileName = nameof(DefaultMeleeWeaponFactorySO), menuName = nameof(MeleeCombat) + "/" + nameof(Implementation) + "/" + nameof(DefaultMeleeWeaponFactorySO))]
    public class DefaultMeleeWeaponFactorySO : MeleeWeaponBaseFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return new DefaultMeleeWeapon(this);
        }
    }
}