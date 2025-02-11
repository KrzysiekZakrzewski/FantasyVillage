using BlueRacconGames.MiniGame.Arrow.Data;
using UnityEngine;

namespace BlueRacconGames.MiniGame.Factory
{
    [CreateAssetMenu(fileName = nameof(ArrowMiniGameFactorySO), menuName = nameof(MiniGame) + "/" + nameof(Factory) + "/" + nameof(ArrowMiniGameFactorySO))]
    public class ArrowMiniGameFactorySO : MiniGameFactorySO
    {
        [field: SerializeField]
        public ArrowDifficultyLevel DifficultyLevel {  get; private set; }
        [field: SerializeField]
        public ArrowDirectionData[] ArrowDirectionDatas { get; private set; }
        public override IMiniGame CreateMiniGame()
        {
            return new ArrowMiniGame(this);
        }
    }
}