using UnityEngine;

namespace BlueRacconGames.MiniGame.Factory
{
    public abstract class MiniGameFactorySO : ScriptableObject, IMiniGameFactory
    {
        [field: SerializeField]
        public int ID { get; private set; }
        public abstract IMiniGame CreateMiniGame();
    }
}
