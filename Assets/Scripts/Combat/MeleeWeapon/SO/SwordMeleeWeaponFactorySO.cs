using Game.Item;
using Game.Item.Factory.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat.Implementation
{
    [CreateAssetMenu(fileName = nameof(SwordMeleeWeaponFactorySO), menuName = nameof(MeleeCombat) + "/" + nameof(Implementation) + "/" + nameof(SwordMeleeWeaponFactorySO))]
    public class SwordMeleeWeaponFactorySO : MeleeWeaponBaseFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return new SwordMeleeWeapon(this);
        }
    }
}
