using UnityEngine;

namespace BlueRacconGames.MiniGame.Arrow.Data
{
    [CreateAssetMenu(fileName = nameof(ArrowDifficultyLevel), menuName = nameof(MiniGame) + "/" + nameof(Data) + "/" + nameof(ArrowDifficultyLevel))]

    public class ArrowDifficultyLevel : ScriptableObject
    {
        [field: SerializeField]
        public int MinArrowsAmount {  get; private set; }
        [field: SerializeField]
        public int MaxArrowsAmount { get; private set; }
        [field: SerializeField]
        public float TimeToEnd {  get; private set; }
    }
}
