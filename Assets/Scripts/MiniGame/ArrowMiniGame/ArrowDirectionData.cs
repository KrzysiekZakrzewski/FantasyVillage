using UnityEngine;
using UnityEngine.InputSystem;

namespace BlueRacconGames.MiniGame.Arrow.Data
{
    [CreateAssetMenu(fileName = nameof(ArrowDirectionData), menuName = nameof(MiniGame) + "/" + nameof(Data) + "/" + nameof(ArrowDirectionData))]
    public class ArrowDirectionData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite ArrowIcon { get; private set; }
        [field: SerializeField]
        public InputAction InputAction {  get; private set; }
    }
}