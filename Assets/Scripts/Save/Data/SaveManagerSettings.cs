using System.Linq;
using UnityEngine;

namespace Saves
{
    [CreateAssetMenu(fileName = nameof(SaveManagerSettings), menuName = nameof(Saves) + "/" + "Assets" + "/" + nameof(SaveManagerSettings))]
    public sealed class SaveManagerSettings : SaveAsset
    {
        [SerializeField]
        private string saveFileName = defaultSaveFileName;

        [field: SerializeField] 
        public bool AutoSaves { private set; get; } = true;

        [field: SerializeField]
        public EncryptionOptionFactorySO EncryptionOptionFactorySO { private set; get; }

        private const string defaultSaveFileName = "saves";
        private const string defaultSaveExtension = ".saveData";

        public string SavePath
        {
            get
            {
                return $"{Application.persistentDataPath}/{saveFileName}{defaultSaveExtension}";
            }
        }

        private void OnValidate()
        {
            if (EncryptionOptionFactorySO == null)
            {
                ShowEncryptionError();
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(saveFileName)) ShowSaveFileError();

                if (string.IsNullOrWhiteSpace(saveFileName)) ShowSaveFileError();

                string trimmed = string.Concat(saveFileName.Where(c => !char.IsWhiteSpace(c)));

                saveFileName = trimmed;
            }
            catch
            {
                ShowSaveFileError();
            }
        }

        private void ShowSaveFileError()
        {
            saveFileName = defaultSaveFileName;

            Debug.LogError($"Invalid save file name: {saveFileName}, in this time used default {defaultSaveFileName}!! \n" +
                $"Please change this object: {this.name}");
        }

        private void ShowEncryptionError()
        {
            Debug.LogError($"Encryption method are not setup, please fix it in: {this.name}");
        }

        public void Initialize()
        {
            OnValidate();
        }
    }
}
