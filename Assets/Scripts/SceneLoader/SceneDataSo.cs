using System.Collections.Generic;
using UnityEngine;

namespace Game.SceneLoader 
{
    [CreateAssetMenu(fileName = nameof(SceneDataSo), menuName = "Game/" + nameof(SceneDataSo))]
    public class SceneDataSo : ScriptableObject
    {
        [field: SerializeField]
        public string SceneName { get; private set; }
        [field: SerializeField]
        public List<string> RequireSceneNames { get; private set; }
        [field: SerializeField]
        public bool WaitForKey { get; private set; }
        [field: SerializeField]
        public bool ShowLoadingScreen { get; private set; }
    }
}