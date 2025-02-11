using Game.Item;
using Game.Item.Factory.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat.Implementation
{
    [CreateAssetMenu(fileName = nameof(FeetMeleeWeaponFactorySO), menuName = nameof(MeleeCombat) + "/" + nameof(Implementation) + "/" + nameof(FeetMeleeWeaponFactorySO))]
    public class FeetMeleeWeaponFactorySO : MeleeWeaponBaseFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return new FeetMeleeWeapon(this);
        }
    }
}