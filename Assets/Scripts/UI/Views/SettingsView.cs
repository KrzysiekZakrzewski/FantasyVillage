using Saves;
using Settings;
using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;
using Zenject;

namespace Game.View
{
    public class SettingsView : BasicView
    {
        [SerializeField]
        private UIToogle musicButton;
        [SerializeField]
        private UIToogle SFXButton;
        [SerializeField]
        private UIToogle vibrationButton;
        [SerializeField]
        private UIButtonBase backButton;

        private SettingsManager settingsManager;

        public override bool Absolute => false;

        [Inject]
        private void Inject(SettingsManager settingsManager)
        {
            this.settingsManager = settingsManager;
        }

        protected override void Awake()
        {
            base.Awake();

            SetupButtons();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            backButton.OnClickE -= OnBackPerformed;
        }

        private void SetupButtons()
        {
            musicButton.SetupButtonEvent(OnMusicPerformed);
            SFXButton.SetupButtonEvent(OnSFXPerformed);
            vibrationButton.SetupButtonEvent(OnVibrationPerformed);
            backButton.OnClickE += OnBackPerformed;
        }

        private void RefreshButtonsState()
        {
            musicButton.LoadButtonState(settingsManager.GetSettingsValue<bool>(SaveKeyUtilities.MusicSettingsKey));
            SFXButton.LoadButtonState(settingsManager.GetSettingsValue<bool>(SaveKeyUtilities.SFXSettingsKey));
            vibrationButton.LoadButtonState(settingsManager.GetSettingsValue<bool>(SaveKeyUtilities.HapticsSettingsKey));
        }

        private void OnBackPerformed()
        {
            ParentStack.TryPopSafe();
        }

        private void OnMusicPerformed()
        {
            settingsManager.SetMusicValue();
        }

        private void OnSFXPerformed()
        {
            settingsManager.SetSfxValue();
        }

        private void OnVibrationPerformed()
        {
            settingsManager.SetVibrationValue();
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            RefreshButtonsState();
        }
    }
}