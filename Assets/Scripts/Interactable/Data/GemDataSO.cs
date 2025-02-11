using UnityEngine;

namespace Interactable.Implementation.Data
{
    [CreateAssetMenu(fileName = nameof(GemDataSO), menuName = nameof(Interactable) + "/" + nameof(Data) + "/" + nameof(GemDataSO))]
    public class GemDataSO : ScriptableObject
    {
        [field: SerializeField]
        public int GemsAmount { get; private set; }
        [field: SerializeField]
        public ParticleSystem PickUpParticle { get; private set; }
    }
}