using Zenject;
using UnityEngine;
using Game.Managers;

namespace Game.Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private MainMenuViewController mainMenuViewController;

        public override void InstallBindings()
        {
            Container.BindInstance(mainMenuViewController).AsSingle();
        }
    }
}