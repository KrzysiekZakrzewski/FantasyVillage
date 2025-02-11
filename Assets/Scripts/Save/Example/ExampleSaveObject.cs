using UnityEngine;

namespace Saves.Example
{
    [CreateAssetMenu(fileName = nameof(ExampleSaveObject), menuName = nameof(Saves) + "/" + "Assets/Objects" + "/" + nameof(ExampleSaveObject))]
    public class ExampleSaveObject : SaveObject
    {
        public SaveValue<string> playerName = new SaveValue<string>("examplePlayerName");
        public SaveValue<int> playerHealth = new SaveValue<int>("examplePlayerHealth");
        public SaveValue<int> playerShield = new SaveValue<int>("examplePlayerShield");
    }
}