using ViewSystem;
using ViewSystem.Implementation;
using UnityEngine;
using Zenject;
using Game.Managers;
using Unity.VisualScripting;

namespace Game.View
{
    public class MainMenuView : BasicView
    {
        [SerializeField]
        private MainMenuManager mainMenuManager;

        [SerializeField]
        private UIButton playButton;
        [SerializeField]
        private UIButton settingsButton;
        [SerializeField]
        private UIButton creditsButton;
        [SerializeField]
        private UIButton quitButton;
        [SerializeField]
        private SettingsView settingsView;
        [SerializeField]
        private CreditsView creditsView;
        [SerializeField]
        private QuitView quitView;

        public override bool Absolute => false;

        protected override void Awake()
        {
            base.Awake();

            SetupButtons();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void SetupButtons()
        {
            playButton.SetupButtonEvent(PlayButton_OnPerformed);
            settingsButton.SetupButtonEvent(SettingsButton_OnPerformed);
            creditsButton.SetupButtonEvent(CreditsButton_OnPerformed);
            quitButton.SetupButtonEvent(TryQuitButton_OnPerformed);
        }

        private void PlayButton_OnPerformed()
        {
            mainMenuManager.Play();
        }

        private void SettingsButton_OnPerformed()
        {
            ParentStack.TryPushSafe(settingsView);
        }

        private void CreditsButton_OnPerformed()
        {
            ParentStack.TryPushSafe(creditsView);
        }

        private void TryQuitButton_OnPerformed()
        {
            ParentStack.TryPushSafe(quitView);
        }

        protected override void Presentation_OnShowPresentationComplete(IAmViewPresentation presentation)
        {
            base.Presentation_OnShowPresentationComplete(presentation);
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);
        }

        public override void NavigateFrom(IAmViewStackItem nextViewStackItem)
        {
            base.NavigateFrom(nextViewStackItem);
        }
    }
}