using UnityEngine;

namespace Interactable.Implementation.Data
{
    [CreateAssetMenu(fileName = nameof(HealDataSO), menuName = nameof(Interactable) + "/" + nameof(Data) + "/" + nameof(HealDataSO))]
    public class HealDataSO : ScriptableObject
    {
        [field: SerializeField]
        public int HealValue { get; private set; }
        [field : SerializeField]
        public ParticleSystem PickUpParticle {  get; private set; }
    }
}
