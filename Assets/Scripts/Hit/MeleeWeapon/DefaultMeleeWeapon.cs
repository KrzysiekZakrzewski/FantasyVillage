using Game.Item.Factory.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class DefaultMeleeWeapon : MeleeWeaponBase
    {
        public DefaultMeleeWeapon(MeleeWeaponBaseFactorySO initialData) : base(initialData)
        {
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}