using UnityEngine;
using Zenject;

namespace Game.SceneLoader
{
    public class BaseSceneLoader : MonoBehaviour
    {
        [SerializeField]
        private SceneDataSo sceneToLoad;

        private SceneLoadManagers sceneLoadManagers;

        [Inject]
        private void Inject(SceneLoadManagers sceneLoadManagers)
        {
            this.sceneLoadManagers = sceneLoadManagers;
        }

        public void LoadScene()
        {
            sceneLoadManagers.LoadLocation(sceneToLoad);
        }
    }
}