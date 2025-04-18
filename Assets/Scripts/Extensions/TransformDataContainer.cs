using UnityEngine;

namespace BlueRacconGames.Extensions
{
    [CreateAssetMenu(fileName = nameof(TransformDataContainer), menuName = nameof(BlueRacconGames) + "/" + nameof(Extensions) + "/" + nameof(TransformDataContainer))]
    public class TransformDataContainer : ScriptableObject
    {
        public TransformData NodeTransformData;
    }
}