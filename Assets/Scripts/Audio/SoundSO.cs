using UnityEngine;

namespace Audio.SoundsData
{
    [CreateAssetMenu(fileName = nameof(SoundSO), menuName = nameof(Audio) + "/" + nameof(Audio.SoundsData) + "/" + nameof(SoundSO))]
    public class SoundSO : ScriptableObject
    {
        [field: SerializeField]
        public AudioTypes AudioType { private set; get; }
        [field: SerializeField]
        public AudioClip Clip { private set; get; }
        [field: SerializeField]
        public bool Loop { private set; get; }
        [field: SerializeField]
        public bool RandomizePitch { private set; get; }
        [field: SerializeField]
        [Range(0f, 1f)]
        public float RandomPitchRangeModifier  = 0.1f;
        [field: SerializeField]
        [Range(0f, 1f)]
        public float Volume = 1f;
        [field: SerializeField]
        [Range(0f, 1f)]
        public float Pitch = 1f;
    }
    public enum AudioTypes
    {
        SFX,
        Music
    }
}