using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class DefaultMeleeCombatController : MeleeCombatControllerBase
    {
        protected override bool CanAttack()
        {
            return true;
        }
    }
}