using UnityEngine;

namespace Damageable.Implementation
{
    [CreateAssetMenu(fileName = nameof(EnemyDamagableDataSO), menuName = nameof(Damageable) + "/" + nameof(Damageable.Implementation) + "/" + nameof(EnemyDamagableDataSO))]
    public class EnemyDamagableDataSO : ScriptableObject, IDamagableDataSO
    {
        [field: SerializeField]
        public int MaxHealth { get; private set; }

        [field: SerializeField]
        public bool ExpireOnDead { get; private set; }
        [field: SerializeField]
        public ParticleSystem ExpireParticle { get; private set; }
    }
}
