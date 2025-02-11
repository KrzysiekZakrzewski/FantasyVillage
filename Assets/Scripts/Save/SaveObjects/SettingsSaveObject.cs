using UnityEngine;

namespace Saves.Object
{
    [CreateAssetMenu(fileName = nameof(SettingsSaveObject), menuName = nameof(Saves) + "/" + "Assets/Objects" + "/" + nameof(SettingsSaveObject))]
    public class SettingsSaveObject : SaveObject
    {
        public SaveValue<bool> MusicValue = new (SaveKeyUtilities.MusicSettingsKey, true);
        public SaveValue<bool> SfxValue = new (SaveKeyUtilities.SFXSettingsKey, true);
        public SaveValue<bool> VibrationValue = new (SaveKeyUtilities.HapticsSettingsKey, true);
    }
}
