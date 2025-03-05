using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace BasicImplementationSample.Scripts
{
    public class DefaultDamagableTarget : MonoBehaviour, IDamagableTarget
    {
        public GameObject GameObject => gameObject;
    }
}
