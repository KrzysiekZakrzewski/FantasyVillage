using UnityEngine;

namespace Saves.Object
{
    [CreateAssetMenu(fileName = nameof(GameStartedSaveObject), menuName = nameof(Saves) + "/" + "Assets/Objects" + "/" + nameof(GameStartedSaveObject))]
    public class GameStartedSaveObject : SaveObject
    {
        public SaveValue<bool> GameStarted = new(SaveKeyUtilities.GameStartedKey);
    }
}
