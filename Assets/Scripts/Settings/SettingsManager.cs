using Saves;
using UnityEngine;
using Audio.Manager;
using Zenject;
using Saves.Object;

namespace Settings
{
    public class SettingsManager : MonoBehaviour
    {
        private SettingsSaveObject settingsSaveObject;
        private AudioManager audioManager;

        public bool InitializeFinished { get; private set; }

        [Inject]
        private void Inject(AudioManager audioManager)
        {
            this.audioManager = audioManager;
        }

        public void LoadSettings() 
        {
            GetSaveObject();

            audioManager.SetSoundGroupMuted(Audio.SoundsData.AudioTypes.Music, GetSettingsValue<bool>(SaveKeyUtilities.MusicSettingsKey));
            audioManager.SetSoundGroupMuted(Audio.SoundsData.AudioTypes.SFX, GetSettingsValue<bool>(SaveKeyUtilities.SFXSettingsKey));

            InitializeFinished = true;
        }

        private void GetSaveObject()
        {
            SaveManager.TryGetSaveObject(out settingsSaveObject);
        }

        private void SetSettingsValue(string key, object value)
        {
            settingsSaveObject.SetValue(key, value);
        }

        public T GetSettingsValue<T>(string key)
        {
            return settingsSaveObject.GetValue<T>(key).Value;
        }

        public void SetMusicValue()
        {
            var isOn = audioManager.SetSoundGroupMuted(Audio.SoundsData.AudioTypes.Music);

            SetSettingsValue(SaveKeyUtilities.MusicSettingsKey, isOn);
        }

        public void SetSfxValue()
        {
            var isOn = audioManager.SetSoundGroupMuted(Audio.SoundsData.AudioTypes.SFX);

            SetSettingsValue(SaveKeyUtilities.SFXSettingsKey, isOn);
        }

        public void SetVibrationValue()
        {

        }

        public void ResetSetings()
        {
            SetSettingsValue(SaveKeyUtilities.MusicSettingsKey, true);
            SetSettingsValue(SaveKeyUtilities.SFXSettingsKey, true);
            SetSettingsValue(SaveKeyUtilities.HapticsSettingsKey, true);

            audioManager.SetSoundGroupMuted(Audio.SoundsData.AudioTypes.Music, GetSettingsValue<bool>(SaveKeyUtilities.MusicSettingsKey));
            audioManager.SetSoundGroupMuted(Audio.SoundsData.AudioTypes.SFX, GetSettingsValue<bool>(SaveKeyUtilities.SFXSettingsKey));
        }
    }
}
