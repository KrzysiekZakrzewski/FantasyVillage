using Game.SceneLoader;
using Saves;
using Saves.Object;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private SceneDataSo hubSceneData;

        private bool gameStarted;

        private SceneLoadManagers sceneLoadManagers;

        [Inject]
        private void Inject(SceneLoadManagers sceneLoadManagers)
        {
            this.sceneLoadManagers = sceneLoadManagers;
        }

        private void Awake()
        {
            GetGameStartedValue();
        }

        public void Play()
        {
            sceneLoadManagers.LoadLocation(hubSceneData);
        }

        private void GetGameStartedValue()
        {
            if (!SaveManager.TryGetSaveObject(out GameStartedSaveObject gameStartedSaveObject)) return;

            gameStarted = gameStartedSaveObject.GetValue<bool>(SaveKeyUtilities.GameStartedKey).Value;
        }
    }
}