using UnityEngine;

namespace Audio.SoundsData
{
    [CreateAssetMenu(fileName = nameof(SoundCollectionSO), menuName = nameof(Audio) + "/" + nameof(Audio.SoundsData) + "/" + nameof(SoundCollectionSO))]
    public class SoundCollectionSO : ScriptableObject
    {
        [SerializeField]
        private SoundSO[] musicSO;

        public SoundSO GetRandomMusic()
        {
            return musicSO[Random.Range(0,musicSO.Length)];
        }
    }
}