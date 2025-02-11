using UnityEngine;
using Random = UnityEngine.Random;

namespace Loading.Data
{
    [CreateAssetMenu(fileName = nameof(LoadingScreenDatabase), menuName = nameof(Loading) + "/" + nameof(Loading.Data) + "/" + nameof(LoadingScreenDatabase))]

    public class LoadingScreenDatabase : ScriptableObject
    {
        [SerializeField]
        private string[] hintsKey;
        [SerializeField]
        private Sprite[] backgrounds;

        public Sprite GetRandomLoadingBackground()
        {
            return backgrounds[Random.Range(0, backgrounds.Length)];
        }

        public string GetRandomHint()
        {
            return hintsKey[Random.Range(0, hintsKey.Length)];
        }
    }
}