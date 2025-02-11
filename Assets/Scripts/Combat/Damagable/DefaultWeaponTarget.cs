using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace BasicImplementationSample.Scripts
{
    public class DefaultWeaponTarget : MonoBehaviour, IWeaponTarget
    {
        public GameObject GameObject => gameObject;
    }
}
